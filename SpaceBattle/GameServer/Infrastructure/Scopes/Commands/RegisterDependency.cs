using GameServer.Infrastructure.Core;
using GameServer.Interfaces;

namespace GameServer.Infrastructure.Scopes.Commands
{
    public class RegisterDependency : ICommand
    {
        string _dependency;
        Func<object[], object> _dependencyResolverStrategy;

        public RegisterDependency(string dependency, Func<object[], object> dependencyResolverStrategy)
        {
            _dependency = dependency;
            _dependencyResolverStrategy = dependencyResolverStrategy;
        }

        public void Execute()
        {
            var currentScope = Ioc.Resolve<IDictionary<string, Func<object[], object>>>("IoC.Scope.Current");
            currentScope.Add(_dependency, _dependencyResolverStrategy);
        }
    }
}
