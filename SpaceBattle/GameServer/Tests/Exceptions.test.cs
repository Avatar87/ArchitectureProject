using GameServer.GameLogic.Commands;
using GameServer.GameLogic.Exceptions;
using GameServer.Interfaces;
using GameServer.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Concurrent;

namespace GameServer.Tests
{
    [TestFixture]
    public class Exceptions
    {
        [Test]
        public void CorrectEnqueueCommands_SingleRetry()
        {
            BlockingCollection<ICommand> queue = new BlockingCollection<ICommand>(100);
            ExceptionHandler.Register(typeof(Move), typeof(ArgumentException), (ICommand cmd, Exception ex) => new Retry(cmd));
            ExceptionHandler.Register(typeof(Retry), typeof(ArgumentException), (ICommand cmd, Exception ex) => new WriteToLog(ex));

            var obj = new Mock<IMovingObject>();
            obj.Setup(s => s.Location).Returns(new Point(1, 2));
            obj.Setup(x => x.Velocity).Returns(new Vector(-1, -3));
            queue.Add(new Move(obj.Object));

            while (queue.Count < 3)
            {
                ICommand cmd = queue.Last();

                try
                {
                    cmd.Execute();
                }
                catch (Exception e)
                {
                    ICommand handler = ExceptionHandler.Handle(cmd, e);
                    queue.Add(handler);
                }
            }
            Assert.That(queue.Count, Is.EqualTo(3));
            Assert.That(queue.Take(), Is.InstanceOf(typeof(Move)));
            Assert.That(queue.Take(), Is.InstanceOf(typeof(Retry)));
            Assert.That(queue.Take(), Is.InstanceOf(typeof(WriteToLog)));
        }

        [Test]
        public void CorrectEnqueueCommands_DoubleRetry()
        {
            BlockingCollection<ICommand> queue = new BlockingCollection<ICommand>(100);
            ExceptionHandler.Register(typeof(Move), typeof(ArgumentException), (ICommand cmd, Exception ex) => new Retry(cmd));
            ExceptionHandler.Register(typeof(Retry), typeof(ArgumentException), (ICommand cmd, Exception ex) => new RetryTwice(cmd));
            ExceptionHandler.Register(typeof(RetryTwice), typeof(ArgumentException), (ICommand cmd, Exception ex) => new WriteToLog(ex));

            var obj = new Mock<IMovingObject>();
            obj.Setup(s => s.Location).Returns(new Point(1, 2));
            obj.Setup(x => x.Velocity).Returns(new Vector(-1, -3));

            queue.Add(new Move(obj.Object));

            while (queue.Count < 4)
            {
                ICommand cmd = queue.Last();

                try
                {
                    cmd.Execute();
                }
                catch (Exception e)
                {
                    ICommand handler = ExceptionHandler.Handle(cmd, e);
                    queue.Add(handler);
                }
            }
            Assert.That(queue.Count, Is.EqualTo(4));
            Assert.That(queue.Take(), Is.InstanceOf(typeof(Move)));
            Assert.That(queue.Take(), Is.InstanceOf(typeof(Retry)));
            Assert.That(queue.Take(), Is.InstanceOf(typeof(RetryTwice)));
            Assert.That(queue.Take(), Is.InstanceOf(typeof(WriteToLog)));
        }
    }
}
