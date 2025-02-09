using GameServer.GameLogic.Commands;
using GameServer.GameLogic.Exceptions;
using GameServer.Interfaces;
using Moq;
using NUnit.Framework;

namespace GameServer.Tests.Commands
{
    [TestFixture]
    public class CheckFuelTests
    {
        [Test]
        public void IncorrectCheckFuel_FuelBurnRateZero()
        {
            var obj = new Mock<IMovingObject>();
            obj.Setup(s => s.Fuel).Returns(10);
            CheckFuel checkFuelCommand = new CheckFuel(obj.Object);
            Assert.Throws<CommandException>(checkFuelCommand.Execute);
        }

        [Test]
        public void CheckFuel_AbleToMove()
        {
            var obj = new Mock<IMovingObject>();
            obj.Setup(s => s.Fuel).Returns(10);
            obj.Setup(x => x.BurnFuelRate).Returns(5);
            CheckFuel checkFuelCommand = new CheckFuel(obj.Object);
            Assert.DoesNotThrow(checkFuelCommand.Execute);
        }

        [Test]
        public void CheckFuel_UnableToMove()
        {
            var obj = new Mock<IMovingObject>();
            obj.Setup(s => s.Fuel).Returns(2);
            obj.Setup(x => x.BurnFuelRate).Returns(3);
            CheckFuel checkFuelCommand = new CheckFuel(obj.Object);
            Assert.Throws<CommandException>(checkFuelCommand.Execute);
        }
    }
}
