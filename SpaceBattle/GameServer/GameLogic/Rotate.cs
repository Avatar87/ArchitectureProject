using GameServer.Enums;
using GameServer.Interfaces;

namespace GameServer.GameLogic
{
    public class Rotate
    {
        private IRotatingObject _obj;

        public Rotate(IRotatingObject o)
        {
            _obj = o;
        }

        public void Execute(Direction d)
        {
            Position newPosition = Position.Undefined;
            switch (_obj.GetPosition())
            {
                case Position.Up:
                    newPosition = d == Direction.Clockwise ? Position.Right : Position.Left;
                    break;
                case Position.Down:
                    newPosition = d == Direction.Clockwise ? Position.Left : Position.Right;
                    break;
                case Position.Right:
                    newPosition = d == Direction.Clockwise ? Position.Down : Position.Up;
                    break;
                case Position.Left:
                    newPosition = d == Direction.Clockwise ? Position.Up : Position.Down;
                    break;
                default:
                    throw new Exception("unable to get position!");
            }

            _obj.SetPosition(newPosition);
        }
    }
}
