using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit;

namespace TwitterClient.Core.Tests.Attributtes
{
    class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(new Fixture()
            .Customize(new AutoMoqCustomization()).Customize(new AutoConfiguredMoqCustomization()))
        {
            
        }
    }
}
