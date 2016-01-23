using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitterClient.Streaming.Exceptions;
using TwitterClient.Streaming.Facade;

namespace TwitterClient.Streaming
{
    public class StreamingUtils : IStreamingUtils
    {
        public async Task<StreamReader> GetReader(HttpRequestMessage request)
        {
            var client = new HttpClient();
            
                HttpResponseMessage webResponse =
                    await
                        client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                        
                 //TODO: проверить статус
                var stream = await webResponse.Content.ReadAsStreamAsync();

                if (stream == null)
                    throw new EmptyStreamException();

                return new StreamReader(stream, Encoding.GetEncoding("utf-8"));
            
        }
    }
}
