using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TwitterClient.Streaming.Facade
{
    public interface IListener
    {
        Task Listen(Func<HttpRequestMessage> requestProvider, Action<string> processRequest);
    }
}
