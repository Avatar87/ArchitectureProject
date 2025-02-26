using GameServer.Interfaces;

namespace GameServer.Infrastructure.Scopes.Commands
{
    public class SetCurrentScope : ICommand
    {
        object _scope;

        public SetCurrentScope(object scope)
        {
            _scope = scope;
        }

        public void Execute()
        {
            InitScopes.currentScopes.Value = _scope;
        }
    }
}
