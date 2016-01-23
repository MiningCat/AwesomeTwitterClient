using System.Collections.Generic;
using System.Xml.Serialization;

namespace TwitterClient.Contracts.Geo
{
    public class Place
    {
        /// <summary>
        /// Place related metadata
        /// </summary>
        [XmlIgnore]
        public Dictionary<string, string> Attributes { get; set; }

        /// <summary>
        /// A bounding box of coordinates which encloses this place.
        /// </summary>
        public Geometry BoundingBox { get; set; }

        /// <summary>
        /// Name of the country containing this place.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Shortened country code representing the country containing this place.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Full human-readable representation of the place’s name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Place ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Short human-readable representation of the place’s name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of place (i.e. neighborhood, city, country, etc.)
        /// </summary>
        public string PlaceType { get; set; }

        /// <summary>
        /// Url to get more details on place
        /// </summary>
        public string Url { get; set; }
    }
}