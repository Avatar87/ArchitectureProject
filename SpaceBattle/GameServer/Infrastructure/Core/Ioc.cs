using GameServer.Infrastructure.Core.Commands;

namespace GameServer.Infrastructure.Core
{
    /// <summary>
    /// Контейнер инверсии зависимостей (Расширяемая фабрика).
    /// </summary>
    public class Ioc
    {
        internal static Func<string, object[], object> _strategy =
            (string dependency, object[] args) =>
            {
                if ("Update Ioc Resolve Dependency Strategy" == dependency)
                {
                    return new UpdateIocResolveDependencyStrategy(
                      (Func<Func<string, object[], object>, Func<string, object[], object>>)args[0]
                    );
                }
                else
                {
                    throw new ArgumentException(@"Dependency {dependency} is not found.");
                }
            };

        public static T Resolve<T>(string dependency, params object[] args)
        {
            return (T)_strategy(dependency, args);
        }
    }
}
