using GameServer.Models;

namespace GameServer.Interfaces
{
    public interface IRotatingObject
    {
        public Angle Angle { get; set; }
        public Angle AngularVelocity { get; set; }
    }
}
