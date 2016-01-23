using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit;
using TwitterClient.Streaming.Tests.Customizations;

namespace TwitterClient.Streaming.Tests.Attributtes
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
