using GameServer.Interfaces;

namespace GameServer.GameLogic.Exceptions
{
    public class CommandException : Exception
    {
        private ICommand _cmd;
        public CommandException()
        { }

        public CommandException(string message)
            : base(message)
        { }

        public CommandException(string message, ICommand cmd)
            : base(message)
        {
            _cmd = cmd;
        }
    }
}
