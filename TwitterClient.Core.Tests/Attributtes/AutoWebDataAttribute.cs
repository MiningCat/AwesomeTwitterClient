using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit;
using TwitterClient.Core.Tests.Customizations;

namespace TwitterClient.Core.Tests.Attributtes
{
    internal class AutoWebDataAttribute : AutoDataAttribute
    {
        public AutoWebDataAttribute() : base(new Fixture()
            .Customize(new AutoMoqCustomization())
            .Customize(new HttpWebRequestCustomization())
            .Customize(new AutoConfiguredMoqCustomization()))
        {

        }
    }
}
