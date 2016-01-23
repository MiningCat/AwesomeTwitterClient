namespace TwitterClient.Contracts.Common.Entities
{
    public class UrlEntity
    {
        // key
        public string UrlAdress { get; set; }
        /// <summary>
        /// Not a valid URL but a string to display instead of the URL
        /// </summary>
        public string DisplayUrl { get; set; }
        /// <summary>
        /// The resolved URL
        /// </summary>
        public string ExpandedUrl { get; set; }
    }
}