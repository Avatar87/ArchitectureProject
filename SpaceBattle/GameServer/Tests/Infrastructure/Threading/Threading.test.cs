using GameServer.Infrastructure.Core;
using GameServer.Infrastructure.Scopes.Commands;
using GameServer.Infrastructure.Threading;
using GameServer.Infrastructure.Threading.Commands;
using Moq;
using NUnit.Framework;
using System.Collections.Concurrent;
using ICommand = GameServer.Interfaces.ICommand;

namespace GameServer.Tests.Infrastructure.Threading
{
    [TestFixture]
    public class ThreadingTests
    {
        [SetUp]
        public void Init()
        {
            new InitScopes().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
            Ioc.Resolve<ICommand>("IoC.Register", "HandleException", (object[] args) => (Exception e) => Console.WriteLine(e.Message)).Execute();
        }

        [Test]
        public void Should_Start_New_Thread_And_HardStop()
        {
            BlockingCollection<ICommand> q = new BlockingCollection<ICommand>(100);
            var st = new ServerThread(q);
            var cmd = new Mock<ICommand>();

            var mre = new ManualResetEvent(false);

            q.Add(cmd.Object);
            q.Add(cmd.Object);
            q.Add(cmd.Object);
            q.Add(new HardStopCommand(st));
            q.Add(cmd.Object);

            Assert.That(st.q.Count, Is.EqualTo(5));

            st.SetActionAfterStop(() => mre.Set());

            st.Start();
            st.Join();

            Assert.That(st.q.Count, Is.EqualTo(1));
            Assert.That(mre.WaitOne(), Is.True);
        }

        [Test]
        public void Should_Start_New_Thread_And_SoftStop()
        {
            BlockingCollection<ICommand> q = new BlockingCollection<ICommand>(100);
            var st = new ServerThread(q);
            var cmd = new Mock<ICommand>();

            var mre = new ManualResetEvent(false);

            q.Add(new SoftStopCommand(st));
            q.Add(cmd.Object);
            q.Add(cmd.Object);
            q.Add(cmd.Object);
            q.Add(cmd.Object);

            Assert.That(st.q.Count, Is.EqualTo(5));

            st.SetActionAfterStop(() => mre.Set());

            st.Start();
            st.Join();

            Assert.That(st.q.Count, Is.EqualTo(0));
            Assert.That(mre.WaitOne(), Is.True);
        }


        [TearDown]
        public void Cleanup()
        {
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
        }
    }
}
