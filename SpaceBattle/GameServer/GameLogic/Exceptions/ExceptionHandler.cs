using GameServer.Interfaces;

namespace GameServer.GameLogic.Exceptions
{
    public class ExceptionHandler
    {
        private static readonly IDictionary<
            Type, IDictionary<Type, Func<ICommand, Exception, ICommand>>
            > _store;

        static ExceptionHandler()
        {
            _store = new Dictionary<
                Type, IDictionary<Type, Func<ICommand, Exception, ICommand>>
                >
            {
            };
        }

        public static ICommand Handle(ICommand command, Exception exception)
        {
            Type commandtype = command.GetType();
            Type exceptionType = exception.GetType();

            return _store[commandtype][exceptionType](command, exception);
        }

        public static void Register(Type commandtype, Type exceptionType, Func<ICommand, Exception, ICommand> handler)
        {
            if (_store.ContainsKey(commandtype))
            {
                _store[commandtype][exceptionType] = handler;
            }
            else
            {
                var dict = new Dictionary<Type, Func<ICommand, Exception, ICommand>>
                {
                    { exceptionType, handler }
                };
                _store.Add(commandtype, dict);
            }
        }
    }
}
