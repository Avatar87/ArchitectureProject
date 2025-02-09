using GameServer.Interfaces;

namespace GameServer.GameLogic.Commands
{
    public class RetryTwice : ICommand
    {

        private ICommand _cmd;
        public RetryTwice(ICommand cmd)
        {
            _cmd = cmd;
        }

        public void Execute()
        {

            _cmd.Execute();
        }
    }
}
