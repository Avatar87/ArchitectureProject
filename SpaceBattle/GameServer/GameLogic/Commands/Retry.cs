using GameServer.Interfaces;

namespace GameServer.GameLogic.Commands
{
    public class Retry : ICommand
    {

        private ICommand _cmd;
        public Retry(ICommand cmd)
        {
            _cmd = cmd;
        }

        public void Execute()
        {

            _cmd.Execute();
        }
    }
}
