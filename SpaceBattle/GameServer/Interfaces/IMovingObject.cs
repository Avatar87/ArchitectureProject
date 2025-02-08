using GameServer.Models;

namespace GameServer.Interfaces
{
    public interface IMovingObject
    {
        public Point Location { get; set; }

        public void SetLocation(Point newLocation);

        public Vector Velocity { get; }
    }
}
