using TwitterClient.Core.Facade;

namespace TwitterClient.Core
{
    public class TwitterCredentials : ITwitterCredentials
    {
        public string ConsumerKey { get; set; }

        public string ConsumerSecret { get; set; }

        public string AccessToken { get; set; }

        public string AccessTokenSecret { get; set; }
    }
}
