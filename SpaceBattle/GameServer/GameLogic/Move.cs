using GameServer.Interfaces;
using GameServer.Models;

namespace GameServer.GameLogic
{
    public class Move
    {
        private IMovingObject _obj;

        public Move(IMovingObject o)
        {
            _obj = o;
        }

        public void Execute()
        {
            Point newLocation = Point.Replace(_obj.GetLocation(), _obj.GetVelocity());
            if (newLocation.X < 0 || newLocation.Y < 0)
            {
                throw new ArgumentException("unable to move out of field!");
            }
            _obj.SetLocation(newLocation);
        }
    }
}
