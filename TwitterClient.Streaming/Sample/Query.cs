using System.Net.Http;
using TwitterClient.Streaming.Facade;

namespace TwitterClient.Streaming.Sample
{
    public class Query : IQuery
    {
        private const string EndpointUrl = "https://stream.twitter.com/1.1/statuses/sample.json";

        public HttpRequestMessage GetRequest() => new HttpRequestMessage(HttpMethod.Get, EndpointUrl);
    }
}
