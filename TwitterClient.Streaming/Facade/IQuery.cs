using System.Net.Http;

namespace TwitterClient.Streaming.Facade
{
    public interface IQuery
    {
        HttpRequestMessage GetRequest();
    }
}
