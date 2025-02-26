using GameServer.Interfaces;

namespace GameServer.Infrastructure.Scopes.Commands
{
    public class ClearCurrentScope : ICommand
    {
        public void Execute()
        {
            InitScopes.currentScopes.Value = null;
        }
    }
}
