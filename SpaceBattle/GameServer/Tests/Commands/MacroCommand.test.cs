using GameServer.GameLogic.Commands;
using GameServer.GameLogic.Exceptions;
using GameServer.Interfaces;
using GameServer.Models;
using Moq;
using NUnit.Framework;

namespace GameServer.Tests.Commands
{
    [TestFixture]
    public class MacroCommandtests
    {
        [Test]
        public void CorrectMacroCommand_BurnFuelAndMove()
        {
            var obj = new Mock<IMovingObject>();
            obj.SetupProperty(s => s.Fuel, 10);
            obj.SetupProperty(s => s.BurnFuelRate, 5);
            obj.SetupProperty(s => s.Location, new Point(12, 5));
            obj.Setup(s => s.Velocity).Returns(new Vector(-7, 3));
            CheckFuel checkFuelCommand = new CheckFuel(obj.Object);
            Move moveCommand = new Move(obj.Object);
            BurnFuel burnFuelCommand = new BurnFuel(obj.Object);
            var commands = new ICommand[]
            {
                checkFuelCommand,
                moveCommand,
                burnFuelCommand
            };
            MacroCommand macroCommand = new MacroCommand(commands, 3);
            Assert.DoesNotThrow(macroCommand.Execute);
            Assert.That(obj.Object.Fuel, Is.EqualTo(5));
            Point newLocation = obj.Object.Location;
            Assert.That(newLocation.X, Is.EqualTo(5));
            Assert.That(newLocation.Y, Is.EqualTo(8));
        }

        [Test]
        public void IncorrectMacroCommand_BurnFuelAndMove()
        {
            var obj = new Mock<IMovingObject>();
            obj.SetupProperty(s => s.Fuel, 3);
            obj.SetupProperty(s => s.BurnFuelRate, 5);
            obj.SetupProperty(s => s.Location, new Point(12, 5));
            obj.Setup(s => s.Velocity).Returns(new Vector(-7, 3));
            CheckFuel checkFuelCommand = new CheckFuel(obj.Object);
            Move moveCommand = new Move(obj.Object);
            BurnFuel burnFuelCommand = new BurnFuel(obj.Object);
            var commands = new ICommand[]
            {
                checkFuelCommand,
                moveCommand,
                burnFuelCommand
            };
            MacroCommand macroCommand = new MacroCommand(commands, 3);
            Assert.Throws<CommandException>(macroCommand.Execute);
        }
    }
}
