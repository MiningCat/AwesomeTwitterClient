using System.Collections.Generic;
using System.Net.Http;
using TwitterClient.Streaming.Filter;

namespace TwitterClient.Streaming.Facade
{
    public interface IFilterQuery : IQuery
    {
        IEnumerable<long> Follow { get; set; }
        IEnumerable<string> Track { get; set; }
        Locations Locations { get; set; }

    }
}
