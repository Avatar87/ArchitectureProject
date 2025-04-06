using GameServer.GameLogic.Commands;
using GameServer.GameLogic.Exceptions;
using GameServer.Infrastructure.Core;
using GameServer.Infrastructure.MessageBroker.Enums;
using GameServer.Infrastructure.MessageBroker.Models;
using GameServer.Interfaces;
using System.Collections.Concurrent;

namespace GameServer.Infrastructure.MessageBroker.Commands
{
    public class Interpret : ICommand
    {
        private GameMessage _msg;
        public Interpret(GameMessage msg)
        {
            _msg = msg;
        }

        public void Execute()
        {
            if (_msg != null)
            {
                if (_msg.OperationId == OperationType.MoveAction)
                {
                    var objects = Ioc.Resolve<IEnumerable<IMovingObject>>("GameObjects");
                    var target = objects.FirstOrDefault(obj => obj.Id == _msg.GameObjectId);
                    if (target != null)
                    {
                        int x = (int)_msg.Args["x"];
                        int y = (int)_msg.Args["y"];
                        target.Velocity.X = x;
                        target.Velocity.Y = y;
                        var cmd = new Move(target);
                        var q = Ioc.Resolve<BlockingCollection<ICommand>>("GameQueue");
                        q.Add(cmd);
                    }
                    else
                    {
                        throw new CommandException("Target object not found!");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Game message is empty!");
            }
        }
    }
}
