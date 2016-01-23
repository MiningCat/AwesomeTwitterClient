using System;
using System.Collections.Generic;
using TwitterClient.Contracts.Common.Entities;
using TwitterClient.Contracts.Geo;

namespace TwitterClient.Contracts.Status
{
    public class Status
    {
        /// <summary>
        /// Tweet Id
        /// </summary>
        public ulong Id { get; set; }

        public IEnumerable<Contributor> Contributors { get; set; }

        /// <summary>
        /// Represents the geographic location of this Tweet as reported by the user or client application. 
        /// The inner coordinates array is formatted as geoJSON (longitude first, then latitude).
        /// </summary>
        public Coordinate Coordinates { get; set; }

        /// <summary>
        /// UTC time when this Tweet was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        // TODO: или entity?
        public Entities Entities { get; set; }

        /// <summary>
        /// Indicates approximately how many times this Tweet has been “favorited” by Twitter users.
        /// </summary>
        public uint? FavoriteCount { get; set; }

        /// <summary>
        /// Indicates the maximum value of the filter_level parameter which may be used and still stream this Tweet.
        /// </summary>
        public FilterLevel FilterLevel { get; set; }

        /// <summary>
        ///  If the represented Tweet is a reply, this field will contain the integer representation of the original Tweet’s ID.
        /// </summary>
        public ulong? InReplyToStatusId { get; set; }

        /// <summary>
        /// When present, indicates a BCP 47 language identifier corresponding to the machine-detected language of the Tweet text,
        ///  or “und” if no language could be detected.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// When present, indicates that the tweet is associated (but not necessarily originating from) a Place.
        /// </summary>
        public IEnumerable<Place> Places { get; set; }

        /// <summary>
        /// it is an indicator that the URL contained in the tweet may contain content or media identified as sensitive content.
        /// </summary>
        public bool? PossiblySensitive { get; set; }

        /// <summary>
        /// Number of times this Tweet has been retweeted.
        /// </summary>
        public uint RetweetCount { get; set; }

        /// <summary>
        /// Retweets can be distinguished from typical Tweets by the existence of a retweeted_status attribute.
        ///  This attribute contains a representation of the original Tweet that was retweeted.
        /// </summary>
        public Status RetweetedStatus { get; set; }

        /// <summary>
        /// where did the tweet come from
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The actual UTF-8 text of the status update
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// has the tweet been truncated
        /// </summary>
        public bool Truncated { get; set; }

        /// <summary>
        /// The user who posted this Tweet.
        /// </summary>
        public User.User User { get; set; }

        /// <summary>
        /// When present and set to “true”, it indicates that this piece of content has been withheld due to a DMCA complaint.
        /// </summary>
        public bool WithheldCopyright { get; set; }

        /// <summary>
        /// When present, indicates a list of uppercase two-letter country codes this content is withheld from. Twitter supports the following non-country values for this field:
        /// “XX” - Content is withheld in all countries 
        /// “XY” - Content is withheld due to a DMCA request.
        /// </summary>
        public List<string> WithheldInCountries { get; set; }

        /// <summary>
        /// When present, indicates whether the content being withheld is the “status” or a “user.”
        /// </summary>
        public string WithheldScope { get; set; }
    }
}