using GameServer.GameLogic.Commands;
using GameServer.Models;
using NUnit.Framework;

namespace GameServer.Tests.Commands
{
    [TestFixture]
    public class MoveTests
    {
        [Test]
        public void CorrectMove()
        {
            SpaceShip ship = new SpaceShip(new Point(12, 5), new Vector(-7, 3), null, null);
            Move moveCommand = new Move(ship);
            moveCommand.Execute();
            Point? newLocation = ship.Location;
            Assert.That(newLocation.X, Is.EqualTo(5));
            Assert.That(newLocation.Y, Is.EqualTo(8));
        }

        [Test]
        public void IncorrectMove_UnableToGetLocation()
        {
            SpaceShip ship = new SpaceShip(null, new Vector(1, 3), null, null);
            Move moveCommand = new Move(ship);
            Assert.Throws<NullReferenceException>(moveCommand.Execute);
        }

        [Test]
        public void IncorrectMove_UnableToGetVelocity()
        {
            SpaceShip ship = new SpaceShip(new Point(5, 5), null, null, null);
            Move moveCommand = new Move(ship);
            Assert.Throws<NullReferenceException>(moveCommand.Execute);
        }

        [Test]
        public void IncorrectMove_UnableToChangePosition()
        {
            SpaceShip ship = new SpaceShip(new Point(1, 2), new Vector(-1, -3), null, null);
            Move moveCommand = new Move(ship);
            Assert.Throws<ArgumentException>(moveCommand.Execute);
        }
    }
}
