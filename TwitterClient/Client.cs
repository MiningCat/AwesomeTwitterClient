using System;
using TwitterClient.Facade;
using TwitterClient.Streaming.Facade;

namespace TwitterClient
{
    public class Client : IClient
    {
        private readonly IStreaming _streaming;
        private readonly Func<FilterStreamQueryBuilder> _filterStreamQueryBuilderProvider;
        private readonly Func<IQuery> _sampleStreamQueryProvider;

        public Client(IStreaming streaming, 
            Func<FilterStreamQueryBuilder> filterStreamQueryBuilderProvider, 
            Func<IQuery> sampleStreamQueryProvider)
        {
            if (streaming == null)
                throw new ArgumentNullException("streaming");
            if (filterStreamQueryBuilderProvider == null)
                throw new ArgumentNullException("filterStreamQueryBuilderProvider");
            if (sampleStreamQueryProvider == null)
                throw new ArgumentNullException("sampleStreamQueryProvider");

            _streaming = streaming;
            _filterStreamQueryBuilderProvider = filterStreamQueryBuilderProvider;
            _sampleStreamQueryProvider = sampleStreamQueryProvider;
        }

        public IObservable<string> GetFilterStream(Func<FilterStreamQueryBuilder, FilterStreamQueryBuilder> queryBuilder)
        {
            if (queryBuilder == null) throw new ArgumentNullException(nameof(queryBuilder));

            var query = queryBuilder(_filterStreamQueryBuilderProvider()).Build();
            return _streaming.GetStream(query);
        }

        public IObservable<string> GetSampleStream()
        {
            var query = _sampleStreamQueryProvider();
            return _streaming.GetStream(query);
        }
    }
}
