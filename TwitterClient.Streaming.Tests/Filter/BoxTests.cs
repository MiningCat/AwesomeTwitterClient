using FluentAssertions;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using TwitterClient.Streaming.Filter;
using Xunit;

namespace TwitterClient.Streaming.Tests.Filter
{
    public class BoxTests
    {
        [Theory, AutoData]
        public void Constructor_Null_GuardClause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(Box));
        }

        [Theory, AutoData]
        public void ToString_AnonymousData_CorrectString(Box sut)
        {
            sut.ToString().Should().Be(sut.BottomLeftPoint + "," + sut.TopRightPoint);
        }
    }
}
