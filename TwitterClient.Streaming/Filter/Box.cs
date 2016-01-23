using System;

namespace TwitterClient.Streaming.Filter
{
    public class Box
    {
        public Box(Point bottomLeftPoint, Point topRightPoint)
        {
            if (bottomLeftPoint == null) throw new ArgumentNullException("bottomLeftPoint");
            if (topRightPoint == null) throw new ArgumentNullException("topRightPoint");

            BottomLeftPoint = bottomLeftPoint;
            TopRightPoint = topRightPoint;
        }

        public Point BottomLeftPoint { get; }
        public Point TopRightPoint { get; }

        public override string ToString() => $"{BottomLeftPoint},{TopRightPoint}";

        public static Box Moscow => new Box(new Point(37.319329, 55.489927), new Point(37.945661, 56.009657));
        /// <summary>
        /// Including Moscow
        /// </summary>
        public static Box MoscowRegion => new Box(new Point(35.149, 54.2543), new Point(40.206, 56.9628));
        public static Box Petersburg => new Box(new Point(30.090332, 59.745216), new Point(30.559783, 60.089675));
        public static Box PetersburgRegion => new Box(new Point(27.74, 58.4147), new Point(35.696, 61.3298));
        public static Box Kiev => new Box(new Point(30.23944, 50.213273), new Point(30.825941, 50.590798));
        public static Box KievRegion => new Box(new Point(29.2664, 49.1791), new Point(32.1607, 51.554));
        public static Box Crimea => new Box(new Point(32.4793, 44.3864), new Point(36.6467, 46.2292));

    }
}
