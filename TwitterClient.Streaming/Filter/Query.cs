using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TwitterClient.Core.Facade;
using TwitterClient.Streaming.Exceptions;
using TwitterClient.Streaming.Facade;

namespace TwitterClient.Streaming.Filter
{
    public class Query : IFilterQuery
    {
        private const string EndpointUrl = "https://stream.twitter.com/1.1/statuses/filter.json";

        //TODO: очень плохо
        private readonly IHttpUtils _oAuthUtils;

        public Query(IHttpUtils oAuthUtils)
        {
            _oAuthUtils = oAuthUtils;
        }

        public IEnumerable<long> Follow { get; set; }

        public IEnumerable<string> Track { get; set; }

        public Locations Locations { get; set; }

        public HttpRequestMessage GetRequest()
        {
            if ((Follow == null || !Follow.Any()) &&
                (Track == null || !Track.Any()) &&
                (Locations == null || !Locations.Boxes.Any()))
                throw new RequiredFilterParametersMissingException();

            var queryString = BuildQueryString();

            return new HttpRequestMessage(HttpMethod.Post, EndpointUrl)
            {
                Content = new StringContent(queryString, Encoding.UTF8, "application/x-www-form-urlencoded")
            };
        }

        private string BuildQueryString()
        {
            var query = new Dictionary<string, string>();
            if (Follow != null && Follow.Any()) query.Add("follow", string.Join(",", Follow));
            if (Track != null && Track.Any()) query.Add("track", string.Join(",", Track));
            if (Locations != null && Locations.Boxes.Any()) query.Add("locations", string.Join(",", Locations));
            return _oAuthUtils.GetParametersString(query, "&");
        }
    }
}
