using GameServer.Interfaces;

namespace GameServer.Infrastructure.Core.Commands
{
    /// <summary>
    /// Команда для обновления стратегии получения зависимостей.
    /// </summary>
    internal class UpdateIocResolveDependencyStrategy : ICommand
    {
        Func<Func<string, object[], object>, Func<string, object[], object>> _updateIoCStrategy;

        public UpdateIocResolveDependencyStrategy(
            Func<Func<string, object[], object>, Func<string, object[], object>> updater
        )
        {
            _updateIoCStrategy = updater;
        }

        public void Execute()
        {
            Ioc._strategy = _updateIoCStrategy(Ioc._strategy);
        }
    }
}
