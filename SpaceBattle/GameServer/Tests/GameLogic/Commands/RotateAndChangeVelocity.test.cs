using GameServer.GameLogic.Commands;
using GameServer.Models;
using NUnit.Framework;

namespace GameServer.Tests.GameLogic.Commands
{
    [TestFixture]
    public class RotateAndChangeVelocityTests
    {
        [Test]
        public void CorrectRotateAndChangeVelocity()
        {
            SpaceShip ship = new SpaceShip(new Point(12, 5), new Vector(3, 0), new Angle(0), new Angle(90));
            RotateAndChangeVelocity rotateCommand = new RotateAndChangeVelocity(ship);
            rotateCommand.Execute();
            Vector newVelocity = ship.Velocity;
            Assert.That(newVelocity, Is.EqualTo(new Vector(0, 3)));
        }

        [Test]
        public void CorrectRotateWithZeroVelocity()
        {
            SpaceShip ship = new SpaceShip(new Point(12, 5), new Vector(0, 0), new Angle(0), new Angle(90));
            RotateAndChangeVelocity rotateCommand = new RotateAndChangeVelocity(ship);
            rotateCommand.Execute();
            Vector newVelocity = ship.Velocity;
            Assert.That(newVelocity, Is.EqualTo(new Vector(0, 0)));
        }

        [Test]
        public void IncorrectRotateAndChangeVelocity_AngleUndefined()
        {
            SpaceShip ship = new SpaceShip(null, new Vector(1, 3), null, new Angle(1, 20));
            RotateAndChangeVelocity rotateCommand = new RotateAndChangeVelocity(ship);
            Assert.Throws<NullReferenceException>(() => rotateCommand.Execute());
        }

        [Test]
        public void IncorrectRotateAndChangeVelocity_AngularVelocityUndefined()
        {
            SpaceShip ship = new SpaceShip(null, new Vector(1, 3), new Angle(1, 20), null);
            RotateAndChangeVelocity rotateCommand = new RotateAndChangeVelocity(ship);
            Assert.Throws<NullReferenceException>(() => rotateCommand.Execute());
        }
    }
}
