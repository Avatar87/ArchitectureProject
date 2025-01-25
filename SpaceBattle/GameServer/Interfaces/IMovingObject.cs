using GameServer.Models;

namespace GameServer.Interfaces
{
    public interface IMovingObject
    {
        public Point GetLocation();

        public void SetLocation(Point newLocation);

        public Vector GetVelocity();
    }
}
