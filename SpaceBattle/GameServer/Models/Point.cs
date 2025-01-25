namespace GameServer.Models
{
    public class Point
    {
        public Point(int x, int y)
        {
            X = x; Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public static Point Replace(Point currentLocation, Vector velocity)
        {
            return new Point(currentLocation.X + velocity.X, currentLocation.Y + velocity.Y);
        }
    }
}
