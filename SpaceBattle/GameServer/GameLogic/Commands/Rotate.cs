using GameServer.Interfaces;

namespace GameServer.GameLogic.Commands
{
    public class Rotate : ICommand
    {
        private IRotatingObject _obj;

        public Rotate(IRotatingObject o)
        {
            _obj = o;
        }

        public void Execute()
        {
            _obj.Angle = (_obj.Angle + _obj.AngularVelocity);
        }
    }
}
