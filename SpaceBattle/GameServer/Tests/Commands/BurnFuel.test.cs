using GameServer.GameLogic.Commands;
using GameServer.GameLogic.Exceptions;
using GameServer.Interfaces;
using Moq;
using NUnit.Framework;

namespace GameServer.Tests.Commands
{
    [TestFixture]
    public class BurnFuelTests
    {
        [Test]
        public void IncorrectBurnFuel_FuelLessThanZero()
        {
            var obj = new Mock<IMovingObject>();
            obj.Setup(s => s.Fuel).Returns(10);
            obj.Setup(s => s.BurnFuelRate).Returns(20);
            BurnFuel burnFuelCommand = new BurnFuel(obj.Object);
            Assert.Throws<CommandException>(burnFuelCommand.Execute);
        }

        [Test]
        public void CorrectBurnFuel()
        {
            var obj = new Mock<IMovingObject>();
            obj.SetupProperty(m => m.Fuel, 10);
            obj.SetupProperty(x => x.BurnFuelRate, 5);
            BurnFuel burnFuelCommand = new BurnFuel(obj.Object);
            burnFuelCommand.Execute();
            Assert.That(obj.Object.Fuel, Is.EqualTo(5));
        }
    }
}
