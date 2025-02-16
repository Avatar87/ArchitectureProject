using GameServer.Interfaces;
using GameServer.Models;

namespace GameServer.GameLogic.Commands
{
    public class RotateAndChangeVelocity : ICommand
    {
        private SpaceShip _obj;

        public RotateAndChangeVelocity(SpaceShip o)
        {
            _obj = o;
        }

        public void Execute()
        {
            _obj.Angle = (_obj.Angle + _obj.AngularVelocity);
            var angleCos = Math.Cos(_obj.AngularVelocity.ToRadians());
            var angleSin = Math.Sin(_obj.AngularVelocity.ToRadians());

            var rotatedX = _obj.Velocity.X * angleCos - _obj.Velocity.Y * angleSin;
            var rotatedY = _obj.Velocity.X * angleSin + _obj.Velocity.Y * angleCos;
            _obj.Velocity.X = (int)rotatedX;
            _obj.Velocity.Y = (int)rotatedY;
        }
    }
}
