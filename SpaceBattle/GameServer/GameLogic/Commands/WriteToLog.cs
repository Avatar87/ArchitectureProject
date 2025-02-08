using GameServer.Interfaces;

namespace GameServer.GameLogic.Commands
{
    public class WriteToLog : ICommand
    {

        private Exception _ex;
        public WriteToLog(Exception ex)
        {
            _ex = ex;
        }

        public void Execute()
        {
            using (StreamWriter logfile = new StreamWriter("./log.txt"))
            {
                logfile.WriteLine(_ex.Message);
            }
        }
    }
}
