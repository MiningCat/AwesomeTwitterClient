using System.Net.Http;

namespace TwitterClient.Core.Facade
{
    public interface IRequestAuthorizer
    {
        HttpRequestMessage AuthorizeRequest(HttpRequestMessage request);
    }
}
