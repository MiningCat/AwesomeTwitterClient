using FluentAssertions;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit;
using TwitterClient.Streaming.Filter;
using Xunit.Extensions;

namespace TwitterClient.Streaming.Tests.Filter
{
    public class PointTests
    {
        [Theory, AutoData]
        public void Constructor_Null_GuardClause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(Point));
        }

        [Theory, AutoData]
        public void ToString_AnonymousData_CorrectString(Point sut)
        {
            sut.ToString().Should().Be(sut.Longitude + "," + sut.Latitude);
        }
    }
}
