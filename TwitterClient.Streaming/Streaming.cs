using System;
using System.Net.Http;
using System.Reactive.Linq;
using TwitterClient.Core.Facade;
using TwitterClient.Streaming.Facade;

namespace TwitterClient.Streaming
{
    public class Streaming : IStreaming
    {
        private readonly IRequestAuthorizer _authorizer;
        private readonly IListener _listener;

        public Streaming(IRequestAuthorizer authorizer, IListener listener)
        {
            if (authorizer == null) throw new ArgumentNullException(nameof(authorizer));
            if (listener == null) throw new ArgumentNullException(nameof(listener));

            _authorizer = authorizer;
            _listener = listener;
        }

        public IObservable<string> GetStream(IQuery query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var requestProvider = GetRequestProvider(query);

            var observable = Observable.Create<string>(async observer =>
            {
                //If exception thrown we retry ten times 
                //TODO: Config
                for (var i = 0; i < 10; i++)
                {
                    try
                    {
                        await _listener.Listen(requestProvider, content => observer.OnNext(content));
                    }
                    catch (Exception exception)
                    {
                        observer.OnError(exception);
                        continue;
                    }
                    break;
                }

                observer.OnCompleted();
            });

            return observable;
        }

        private Func<HttpRequestMessage> GetRequestProvider(IQuery query)
        {
            Func<HttpRequestMessage> requestProvider = () =>
            {
                var request = query.GetRequest();
                return _authorizer.AuthorizeRequest(request);
            };
            return requestProvider;
        }
    }
}
