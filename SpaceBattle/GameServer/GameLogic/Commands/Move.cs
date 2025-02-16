using GameServer.Interfaces;
using GameServer.Models;

namespace GameServer.GameLogic.Commands
{
    public class Move : ICommand
    {
        private IMovingObject _obj;

        public Move(IMovingObject o)
        {
            _obj = o;
        }

        public void Execute()
        {
            Point newLocation = Point.Replace(_obj.Location, _obj.Velocity);
            if (newLocation.X < 0 || newLocation.Y < 0)
            {
                throw new ArgumentException("unable to move out of field!");
            }
            _obj.Location = newLocation;
        }
    }
}
