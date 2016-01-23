using System.Linq;
using FluentAssertions;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit;
using TwitterClient.Streaming.Filter;
using Xunit.Extensions;

namespace TwitterClient.Streaming.Tests.Filter
{
    public class LocationsTests
    {
        [Theory, AutoData]
        public void Constructor_Null_GuardClause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(Locations));
        }

        [Theory, AutoData]
        public void ToString_AnonymousData_CorrectString(Locations sut)
        {
            sut.ToString().Should().Be(string.Join(",", sut.Boxes.Select(s => s.ToString())));
        }

        [Theory, AutoData]
        public void Concat_AnonymousData_BoxesConcated(Locations sut, Locations locations)
        {
            var result = sut.Concat(locations);

            result.Boxes.ShouldAllBeEquivalentTo(sut.Boxes.Concat(locations.Boxes));
        }
    }
}
