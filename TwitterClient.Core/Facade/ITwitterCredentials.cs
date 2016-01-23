namespace TwitterClient.Core.Facade
{
    public interface ITwitterCredentials
    {
        string ConsumerKey { get; }
        string ConsumerSecret { get; }
        string AccessToken { get; }
        string AccessTokenSecret { get; }
    }
}
