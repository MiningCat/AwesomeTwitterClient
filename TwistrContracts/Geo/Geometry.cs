using System.Collections.Generic;

namespace TwitterClient.Contracts.Geo
{
    public class Geometry
    {
        /// <summary>
        /// Type of bouding box
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Coordinates for bounding box
        /// </summary>
        public List<Coordinate> Coordinates { get; set; }
    }
}