using System;

namespace TwitterClient.Streaming.Facade
{
    public interface IStreaming
    {
        IObservable<string> GetStream(IQuery query);
    }
}