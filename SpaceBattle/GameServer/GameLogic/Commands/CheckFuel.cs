using GameServer.GameLogic.Exceptions;
using GameServer.Interfaces;

namespace GameServer.GameLogic.Commands
{
    public class CheckFuel : ICommand
    {

        private IMovingObject _obj;
        public CheckFuel(IMovingObject o)
        {
            _obj = o;
        }

        public void Execute()
        {
            if (_obj.BurnFuelRate == 0)
            {
                throw new CommandException("incorrect BurnFuelRate!", this);
            }
            if (_obj.Fuel - _obj.BurnFuelRate < 0)
            {
                throw new CommandException("not enough fuel!", this);
            }
        }
    }
}
