using System.Net;
using System.Net.Http;
using Ploeh.AutoFixture;

namespace TwitterClient.Core.Tests.Customizations
{
    class HttpWebRequestCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, @"http://localhost/");
            fixture.Register(() => request);
        }
    }
}
