using GameServer.Models;

namespace GameServer.Interfaces
{
    public interface IRotatingObject : IObject
    {
        public Angle Angle { get; set; }
        public Angle AngularVelocity { get; set; }
    }
}
