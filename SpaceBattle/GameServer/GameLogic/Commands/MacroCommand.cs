using GameServer.GameLogic.Exceptions;
using GameServer.Interfaces;

namespace GameServer.GameLogic.Commands
{
    public class MacroCommand : ICommand
    {

        private ICommand[] _commands;

        private int _count;
        public MacroCommand(ICommand[] commandsArray, int count)
        {
            _commands = commandsArray;
            _count = count;
        }

        public void Execute()
        {
            for (int i = 0; i < _count; i++)
            {
                try
                {
                    _commands[i].Execute();
                }
                catch (Exception e)
                {
                    throw new CommandException(e.Message, this);
                }
            }
        }
    }
}
