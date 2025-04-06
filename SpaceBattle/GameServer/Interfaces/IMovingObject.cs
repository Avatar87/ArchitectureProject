using GameServer.Models;

namespace GameServer.Interfaces
{
    public interface IMovingObject : IObject
    {
        public Point Location { get; set; }

        public Vector Velocity { get; }

        public int Fuel { get; set; }

        public int BurnFuelRate { get; set; }
    }
}
