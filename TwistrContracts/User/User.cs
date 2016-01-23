using System;
using System.Collections.Generic;

namespace TwitterClient.Contracts.User
{
    public class User
    {
        /// <summary>
        /// User Id
        /// </summary>
        public ulong Id { get; set; }

        /// <summary>
        /// Indicates that the user has an account with “contributor mode” enabled,
        ///  allowing for Tweets issued by the user to be co-authored by another account. 
        /// </summary>
        public bool ContributorsEnabled { get; set; }

        /// <summary>
        /// The UTC datetime that the user account was created on Twitter.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// When true, indicates that the user has not altered the theme or background of their user profile.
        /// </summary>
        public bool DefaultProfile { get; set; }

        /// <summary>
        /// When true, indicates that the user has not uploaded their own avatar and a default egg avatar is used instead.
        /// </summary>
        public bool DefaultProfileImage { get; set; }

        /// <summary>
        /// The user-defined UTF-8 string describing their account.
        /// </summary>
        public string Description { get; set; }

        // TODO: Entities

        /// <summary>
        /// The number of tweets this user has favorited in the account’s lifetime.
        /// </summary>
        public uint FavouritesCount { get; set; }

        /// <summary>
        /// The number of followers this account currently has.
        /// </summary>
        public uint FollowersCount { get; set; }

        /// <summary>
        /// The number of users this account is following (AKA their “followings”).
        /// </summary>
        public uint FriendsCount { get; set; }

        /// <summary>
        /// When true, indicates that the user has enabled the possibility of geotagging their Tweets.
        /// </summary>
        public bool GeoEnabled { get; set; }

        /// <summary>
        /// The BCP 47 code for the user’s self-declared user interface language. May or may not have anything to do with the content of their Tweets.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Number of lists user is a member of
        /// </summary>
        public int ListedCount { get; set; }

        /// <summary>
        /// The user-defined location for this account’s profile. Not necessarily a location nor parseable.
        ///  This field will occasionally be fuzzily interpreted by the Search service.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// The name of the user, as they’ve defined it. Not necessarily a person’s name.
        ///  Typically capped at 20 characters, but subject to change.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The hexadecimal color chosen by the user for their background.
        /// </summary>
        public string ProfileBackgroundColor { get; set; }

        /// <summary>
        /// URL of profile background image
        /// </summary>
        public string ProfileBackgroundImageUrl { get; set; }

        /// <summary>
        /// The HTTPS-based URL pointing to the standard web representation of the user’s uploaded profile banner.
        /// </summary>
        public string ProfileBannerUrl { get; set; }

        /// <summary>
        /// A HTTP-based URL pointing to the user’s avatar image.
        /// </summary>
        public string ProfileImageUrl { get; set; }

        /// <summary>
        /// A HTTPS-based URL pointing to the user’s avatar image.
        /// </summary>
        public string ProfileImageUrlHttps { get; set; }

        /// <summary>
        /// When true, indicates that this user has chosen to protect their Tweets.
        /// </summary>
        public bool Protected { get; set; }

        /// <summary>
        /// The screen name, handle, or alias that this user identifies themselves with. 
        /// </summary>
        public string ScreenName { get; set; }

        /// <summary>
        /// The number of tweets (including retweets) issued by the user.
        /// </summary>
        public uint StatusesCount { get; set; }

        /// <summary>
        /// Time Zone
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// A URL provided by the user in association with their profile.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The offset from GMT/UTC in seconds.
        /// </summary>
        public int? UtcOffset { get; set; }

        /// <summary>
        /// When true, indicates that the user has a verified account.
        /// </summary>
        public bool Verified { get; set; }

        /// <summary>
        /// When present, indicates a textual representation of the two-letter country codes this user is withheld from.
        /// </summary>
        public IEnumerable<string> WithheldInCountries { get; set; }

        /// <summary>
        /// When present, indicates whether the content being withheld is the “status” or a “user.”
        /// </summary>
        public string WithheldScope { get; set; }
    }
}