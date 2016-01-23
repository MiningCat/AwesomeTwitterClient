using System;

namespace TwitterClient.Facade
{
    public interface IClient
    {
        IObservable<string> GetFilterStream(Func<FilterStreamQueryBuilder, FilterStreamQueryBuilder> queryBuilder);
        IObservable<string> GetSampleStream();
    }
}
