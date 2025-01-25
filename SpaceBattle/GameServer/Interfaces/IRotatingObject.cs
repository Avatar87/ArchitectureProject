using GameServer.Enums;

namespace GameServer.Interfaces
{
    public interface IRotatingObject
    {
        public Position GetPosition();

        public void SetPosition(Position newPosition);
    }
}
