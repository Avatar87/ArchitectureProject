using GameServer.Infrastructure.Core;
using GameServer.Interfaces;
using System.Collections.Concurrent;

namespace GameServer.Infrastructure.Threading
{
    public class ServerThread
    {
        public BlockingCollection<ICommand> q;
        public Action behaviour;
        public Action actionAfterStop = () => { };

        private Thread _t;
        private bool _stop = false;

        private void DefaultBehaviour()
        {
            var cmd = q.Take();
            try
            {
                cmd.Execute();
            }
            catch (Exception e)
            {
                Ioc.Resolve<ICommand>("HandleException", cmd, e).Execute();
            }
        }

        public ServerThread(BlockingCollection<ICommand> newQ)
        {
            q = newQ;
            behaviour = DefaultBehaviour;

            _t = new Thread(() =>
            {
                while (!_stop)
                {
                    behaviour();
                }
                actionAfterStop();
            });

        }

        public void Start()
        {
            _t.Start();
        }

        public void Stop()
        {
            _stop = true;
        }

        public void Join()
        {
            _t.Join();
        }

        public void SetBehaviour(Action newBeh)
        {
            behaviour = newBeh;
        }
        public void SetActionAfterStop(Action newAction)
        {
            actionAfterStop = newAction;
        }
    }
}
