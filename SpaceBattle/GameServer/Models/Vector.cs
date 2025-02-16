namespace GameServer.Models
{
    public class Vector
    {
        public Vector(int x, int y)
        {
            X = x; Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Vector)
            {
                Vector other = (Vector)obj;
                return X == other.X && Y == other.Y;
            }
            else
            {
                return false;
            }
        }
    }
}
