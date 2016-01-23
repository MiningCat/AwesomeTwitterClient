using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitterClient.Streaming.Filter
{
    public class Locations
    {
        public Locations(IEnumerable<Box> boxes)
        {
            if (boxes == null) throw new ArgumentNullException("boxes");
            Boxes = boxes;
        }

        public IEnumerable<Box> Boxes { get; }

        public override string ToString()
        {
            return string.Join(",", Boxes);
        }

        /// <summary>
        /// Without Crimea =)
        /// </summary>
        public static Locations Russia => new Locations(new[]
        {
            new Box(new Point(31.59, 53.7), new Point(-169.05, 81.86)),
            new Box(new Point(32.03,49.56), new Point(61.78,53.7)),
            new Box(new Point(78.22,49.56), new Point(118.86,53.79)),
            new Box(new Point(127.04,49.28), new Point(164.43,53.79)),
            new Box(new Point(134.78,44.76), new Point(156.7,51.87)),
            new Box(new Point(131.22,41.09), new Point(137.85,45.07)),
            new Box(new Point(29.0819,66.8392), new Point(35.53,69.54)),
            new Box(new Point(27.63,56.81), new Point(37.49,60.97)),
            new Box(new Point(30.93,54.51), new Point(41.04,57.42)),
            new Box(new Point(31.78,52.23), new Point(41.9,55.15)),
            new Box(new Point(35.43,50.39), new Point(42.76,52.4)),
            new Box(new Point(39.98,43.0), new Point(48.69,50.09)),
            new Box(new Point(39.98,43.0), new Point(48.69,50.09)),
            new Box(new Point(36.79,43), new Point(41.68,47.43))
        });

        public Locations Concat(Locations locations)
        {
            if (locations == null)
                throw new ArgumentNullException("locations");

            return new Locations(Boxes.Concat(locations.Boxes));
        }
    }
}
