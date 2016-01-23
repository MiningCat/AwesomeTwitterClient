﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using TwitterClient.Core.Facade;
using TwitterClient.Streaming.Facade;

namespace TwitterClient.Streaming
{
    public class Listener : IListener
    {
        private readonly IStreamingUtils _streamingUtils;
        private readonly IHttpUtils _httpUtils;

        public Listener(IStreamingUtils streamingUtils, IHttpUtils httpUtils)
        {
            if (streamingUtils == null)
                throw new ArgumentNullException("streamingUtils");
            if (httpUtils == null)
                throw new ArgumentNullException("httpUtils");

            _streamingUtils = streamingUtils;
            _httpUtils = httpUtils;
        }

        public async Task Listen(Func<HttpRequestMessage> requestProvider, Action<string> processRequest)
        {
            if (requestProvider == null)
                throw new ArgumentNullException(nameof(requestProvider));
            if (processRequest == null)
                throw new ArgumentNullException(nameof(processRequest));

            using (var request = requestProvider())
            {
                var streamReader = await _streamingUtils.GetReader(request).ConfigureAwait(false);

                while (!streamReader.EndOfStream)
                {
                    var json = await streamReader.ReadLineAsync();
                    processRequest(_httpUtils.UnescapeUnicode(json));
                }
            }
        }

        //TODO: Найти способ слить эти методы
        public async Task Listen(Func<HttpRequestMessage> requestProvider, Func<string, Task> processRequest)
        {
            if (requestProvider == null)
                throw new ArgumentNullException(nameof(requestProvider));
            if (processRequest == null)
                throw new ArgumentNullException(nameof(processRequest));

            using (var request = requestProvider())
            {

                var streamReader = await _streamingUtils.GetReader(request).ConfigureAwait(false);

                while (!streamReader.EndOfStream)
                {
                    var json = await streamReader.ReadLineAsync();
                    await processRequest(_httpUtils.UnescapeUnicode(json)).ConfigureAwait(false);
                }
            }
        }
    }
}
