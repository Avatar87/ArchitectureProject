using GameServer.Models;

namespace GameServer.Interfaces
{
    public interface IRotatingObject
    {
        public Angle GetAngularVelocity();

        public void SetAngle(Angle angle);

        public Angle GetAngle();
    }
}
