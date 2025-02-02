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

        public void Execute()
        {
            _obj.SetAngle(_obj.GetAngle() + _obj.GetAngularVelocity());
        }
    }
}
