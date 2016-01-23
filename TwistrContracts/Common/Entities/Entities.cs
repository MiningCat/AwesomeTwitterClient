using System.Collections.Generic;

namespace TwitterClient.Contracts.Common.Entities
{
    /// <summary>
    /// Entities which have been parsed out of the text of the Tweet.
    /// </summary>
    public class Entities
    {
        public IEnumerable<string> HashTags { get; set; }
        public IEnumerable<UrlEntity> Urls { get; set; }
        /// <summary>
        /// Список упомянутых пользователей
        /// </summary>
        public IEnumerable<ulong> UsersMentions { get; set; }
    }
}