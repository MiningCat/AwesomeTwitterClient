using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit;
using TwitterClient.Core;
using TwitterClient.Core.Facade;
using TwitterClient.Streaming.Exceptions;
using TwitterClient.Streaming.Filter;
using Xunit;
using Xunit.Extensions;

namespace TwitterClient.Streaming.Tests.Filter
{
    public class QueryTests
    {
        [Fact]
        public void GetRequest_NullFilterTrackAndLocations_RequiredFilterParametersMissingException()
        {
            var sut = new Query(Mock.Of<IHttpUtils>());

            sut.Invoking(a => a.GetRequest()).ShouldThrow<RequiredFilterParametersMissingException>();
        }

        [Fact]
        public void GetRequest_NullFilterLocationsAndEmtyTrack_RequiredFilterParametersMissingException()
        {
            var sut = new Query(Mock.Of<IHttpUtils>()) {Track = new List<string>()};

            sut.Invoking(a => a.GetRequest()).ShouldThrow<RequiredFilterParametersMissingException>();
        }

        [Theory]
        [InlineData("1", "2", null, "track=1%2c2")]
        [InlineData("Moscow", "Run", "Marathon", "track=Moscow%2cRun%2cMarathon")]
        [InlineData("Putin Dickhead", null, null, "track=Putin%20Dickhead")]
        [InlineData("Путин", null, null, "track=%D0%9F%D1%83%D1%82%D0%B8%D0%BD")]
        public void GetRequest_DifferentTracks_CorrectQuery(string track1, string track2, string track3, string result)
        {
            var tracks = new List<string>();
            if (track1 != null) tracks.Add(track1);
            if (track2 != null) tracks.Add(track2);
            if (track3 != null) tracks.Add(track3);
            var sut = new Query(new HttpUtils()) { Track = tracks };

            var request = sut.GetRequest();

            var content = request.Content.ReadAsStringAsync().Result;
            content.ToLower().Should().Be(result.ToLower());
        }

        [Theory, AutoData]
        public void GetRequest_AnonymousFollow_CorrectQuery(List<long> follow)
        {
            var sut = new Query(new HttpUtils()) { Follow = follow };

            var request = sut.GetRequest();

            var content = request.Content.ReadAsStringAsync().Result;
            content.ToLower().Should().Be("follow=" + string.Join("%2c", follow));
        }

        [Theory, AutoData]
        public void GetRequest_AnonymousLocations_CorrectQuery(Locations locations)
        {
            var sut = new Query(new HttpUtils()) { Locations = locations };

            var request = sut.GetRequest();

            var content = request.Content.ReadAsStringAsync().Result;
            content.ToLower().Should().Be("locations=" + locations.ToString().Replace(",", "%2c"));
        }

        [Theory, AutoData]
        public void GetRequest_AnonymousData_ContainAllParameters(Locations locations, List<long> follow, List<string> track)
        {
            var sut = new Query(new HttpUtils()) { Locations = locations, Follow = follow, Track = track};

            var request = sut.GetRequest();

            var content = request.Content.ReadAsStringAsync().Result;
            content.Should().Contain("locations=");
            content.Should().Contain("follow=");
            content.Should().Contain("track=");
        }
    }
}
