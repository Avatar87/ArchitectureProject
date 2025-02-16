using GameServer.GameLogic.Exceptions;
using GameServer.Interfaces;

namespace GameServer.GameLogic.Commands
{
    public class BurnFuel : ICommand
    {

        private IMovingObject _obj;
        public BurnFuel(IMovingObject o)
        {
            _obj = o;
        }

        public void Execute()
        {
            if (_obj.Fuel - _obj.BurnFuelRate < 0)
            {
                throw new CommandException("fuel can not be less than zero!", this);
            }
            _obj.Fuel -= _obj.BurnFuelRate;
        }
    }
}
