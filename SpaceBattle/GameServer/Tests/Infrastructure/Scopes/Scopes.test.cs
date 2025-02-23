using GameServer.Infrastructure.Core;
using GameServer.Infrastructure.Scopes.Commands;
using GameServer.Interfaces;
using NUnit.Framework;

namespace GameServer.Tests.Infrastructure.Scopes
{
    [TestFixture]
    public class ScopesTests
    {
        [SetUp]
        public void Init()
        {
            new InitScopes().Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
        }

        [Test]
        public void Ioc_Should_Resolve_Registered_Dependency_In_CurrentScope()
        {
            Ioc.Resolve<ICommand>("IoC.Register", "someDependency", (object[] args) => (object)1).Execute();
            Assert.That(1, Is.EqualTo(Ioc.Resolve<int>("someDependency")));
        }

        [Test]
        public void Ioc_Should_Throw_Exception_On_Unregistered_Dependency_In_CurrentScope()
        {
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Current");
            Assert.Throws<Exception>(() => Ioc.Resolve<int>("someDependency", iocScope));
        }

        [Test]
        public void Ioc_Should_Use_Parent_Scope_If_Resolving_Dependency_Is_Not_Defined_In_Current_Scope()
        {
            Ioc.Resolve<ICommand>("IoC.Register", "someDependency", (object[] args) => (object)1).Execute();
            var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

            Assert.That(iocScope, Is.EqualTo(Ioc.Resolve<object>("IoC.Scope.Current")));
            Assert.That(1, Is.EqualTo(Ioc.Resolve<int>("someDependency")));
        }

        [Test]
        public void Different_Threads_Have_Different_Current_Scope()
        {
            Thread thread1 = new Thread(() =>
            {
                var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
                Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
                Assert.That(iocScope, Is.EqualTo(Ioc.Resolve<object>("IoC.Scope.Current")));
                Assert.DoesNotThrow(() => Ioc.Resolve<object>("IoC.Scope.Parent"));
            });

            Thread thread2 = new Thread(() =>
            {
                Assert.Throws<Exception>(() => Ioc.Resolve<object>("IoC.Scope.Parent"));
            });

            thread1.Start();
            thread1.Join();
            thread2.Start();
            thread2.Join();
        }

        [TearDown]
        public void Cleanup()
        {
            Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
        }
    }
}
