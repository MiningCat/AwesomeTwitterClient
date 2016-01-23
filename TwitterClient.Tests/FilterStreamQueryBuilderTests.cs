using System.Linq;
using FluentAssertions;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using TwitterClient.Core;
using TwitterClient.Streaming.Filter;
using TwitterClient.Tests.Attributtes;
using Xunit;

namespace TwitterClient.Tests
{
    public class FilterStreamQueryBuilderTests
    {
        [Theory, AutoMoqData]
        public void AllMethods_Null_GuardClause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(FilterStreamQueryBuilder));
        }

        [Theory, AutoMoqData]
        public void FilterByTrack_AnonymousData_CorrectQuery(string[] keyWords)
        {
            var query = new Query(new HttpUtils());
            var sut = new FilterStreamQueryBuilder(query);

            sut.FilterByTrack(keyWords);

            query.Track.ShouldBeEquivalentTo(keyWords);
        }

        [Theory, AutoMoqData]
        public void FilterByTrack_AnonymousDataNotEmptyQuery_CorrectQuery(string[] keyWords, 
            [Frozen(Matching.ImplementedInterfaces)]Query query,
            FilterStreamQueryBuilder sut)
        {
            var oldValue = query.Track.ToList();

            sut.FilterByTrack(keyWords);

            query.Track.ShouldBeEquivalentTo(oldValue.Concat(keyWords));
        }


        [Theory, AutoMoqData]
        public void FilterByFollowers_AnonymousData_CorrectQuery(long[] folowwers)
        {
            var query = new Query(new HttpUtils());
            var sut = new FilterStreamQueryBuilder(query);

            sut.FilterByFillowers(folowwers);

            query.Follow.ShouldBeEquivalentTo(folowwers);
        }

        [Theory, AutoMoqData]
        public void FilterByFollowers_AnonymousDataNotEmptyQuery_CorrectQuery(long[] folowwers,
            [Frozen(Matching.ImplementedInterfaces)]Query query,
            FilterStreamQueryBuilder sut)
        {
            var oldValue = query.Follow.ToList();

            sut.FilterByFillowers(folowwers);

            query.Follow.ShouldBeEquivalentTo(oldValue.Concat(folowwers));
        }

        [Theory, AutoMoqData]
        public void FilterByLocations_AnonymousData_CorrectQuery(Locations location)
        {
            var query = new Query(new HttpUtils());
            var sut = new FilterStreamQueryBuilder(query);

            sut.FilterByLocations(location);

            query.Locations.ShouldBeEquivalentTo(location);
        }

        [Theory, AutoMoqData]
        public void FilterByLocations_AnonymousDataNotEmptyQuery_CorrectQuery(Locations location,
            [Frozen(Matching.ImplementedInterfaces)]Query query,
            FilterStreamQueryBuilder sut)
        {
            var oldValue = query.Locations;

            sut.FilterByLocations(location);

            query.Locations.ShouldBeEquivalentTo(oldValue.Concat(location));
        }

        public void FilterByLocationsBox_AnonymousData_CorrectQuery(Box box)
        {
            var query = new Query(new HttpUtils());
            var sut = new FilterStreamQueryBuilder(query);

            sut.FilterByLocations(box);

            query.Locations.ShouldBeEquivalentTo(new Locations(new[] { box }));
        }

        [Theory, AutoMoqData]
        public void FilterByLocationsBox_AnonymousDataNotEmptyQuery_CorrectQuery(Box box,
            [Frozen(Matching.ImplementedInterfaces)]Query query,
            FilterStreamQueryBuilder sut)
        {
            var oldValue = query.Locations;

            sut.FilterByLocations(box);

            query.Locations.ShouldBeEquivalentTo(oldValue.Concat(new Locations(new[] { box })));
        }
    }
}
