using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace TwitterClient.Streaming.Facade
{
    public interface IStreamingUtils
    {
        Task<StreamReader> GetReader(HttpRequestMessage request);

    }
}
