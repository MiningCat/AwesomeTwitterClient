namespace TwitterClient.Streaming.Filter
{
    public class Point
    {
        public Point(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public double Longitude { get; }
        public double Latitude { get; }

        public override string ToString() => $"{Longitude},{Latitude}";
    }
}
