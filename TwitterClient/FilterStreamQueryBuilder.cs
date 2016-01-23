using System;
using System.Collections.Generic;
using System.Linq;
using TwitterClient.Streaming.Facade;
using TwitterClient.Streaming.Filter;

namespace TwitterClient
{
    public class FilterStreamQueryBuilder
    {
        private readonly IFilterQuery _filterQuery;
        public FilterStreamQueryBuilder(IFilterQuery filterQuery)
        {
            if (filterQuery == null) throw new ArgumentNullException("filterQuery");
            _filterQuery = filterQuery;
        }

        public FilterStreamQueryBuilder FilterByTrack(params string[] keyWords)
        {
            return FilterByTrack((IEnumerable<string>)keyWords);
        }

        public FilterStreamQueryBuilder FilterByTrack(IEnumerable<string> keyWords)
        {
            if (keyWords == null) throw new ArgumentNullException("keyWords");
            _filterQuery.Track = _filterQuery.Track?.Concat(keyWords) ?? keyWords;
            return this;
        }

        public FilterStreamQueryBuilder FilterByFillowers(params long[] followers)
        {
            return FilterByFillowers((IEnumerable<long>) followers);
        }

        public FilterStreamQueryBuilder FilterByFillowers(IEnumerable<long> followers)
        {
            if (followers == null) throw new ArgumentNullException("followers");
            _filterQuery.Follow = _filterQuery.Follow?.Concat(followers) ?? followers;
            return this;
        }

        public FilterStreamQueryBuilder FilterByLocations(Locations location)
        {
            if (location == null) throw new ArgumentNullException("location");
            _filterQuery.Locations = _filterQuery.Locations?.Concat(location) ?? location;
            return this;
        }

        public FilterStreamQueryBuilder FilterByLocations(Box box)
        {
            if (box == null) throw new ArgumentNullException("box");
            var locations = new Locations(new[] {box});
            _filterQuery.Locations = _filterQuery.Locations?.Concat(locations) ?? locations;
            return this;
        }

        internal IFilterQuery Build()
        {
            return _filterQuery;
        }
    }
}
