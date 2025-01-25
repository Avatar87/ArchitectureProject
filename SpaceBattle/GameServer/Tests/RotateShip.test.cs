using GameServer.Enums;
using GameServer.GameLogic;
using GameServer.Models;
using NUnit.Framework;

namespace GameServer.Tests
{
    [TestFixture]
    public class RotateShip
    {
        [Test]
        public void CorrectRotate()
        {
            SpaceShip ship = new SpaceShip(new Point(12, 5), new Vector(-7, 3), Position.Up);
            Rotate rotateCommand = new Rotate(ship);
            rotateCommand.Execute(Direction.Clockwise);
            Position newPosition = ship.GetPosition();
            Assert.That(newPosition, Is.EqualTo(Position.Right));
        }

        [Test]
        public void IncorrectRotate_PositionUndefined()
        {
            SpaceShip ship = new SpaceShip(null, new Vector(1, 3), Position.Undefined);
            Rotate rotateCommand = new Rotate(ship);
            Assert.Throws<Exception>(() => rotateCommand.Execute(Direction.Clockwise));
        }
    }
}
