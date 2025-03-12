
using GameServer.Interfaces;

namespace GameServer.Infrastructure.Threading.Commands
{
    public class SoftStopCommand : ICommand
    {
        private ServerThread _st;
        public SoftStopCommand(ServerThread st)
        {
            _st = st;
        }

        public void Execute()
        {
            Action oldBehaviour = _st.behaviour;
            _st.SetBehaviour(() =>
            {
                if (_st.q.Count > 0)
                {
                    oldBehaviour();
                }
                else
                {
                    _st.Stop();
                }
            });
        }
    }
}
