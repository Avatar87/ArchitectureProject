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
            SpaceShip ship = new SpaceShip(new Point(12, 5), new Vector(-7, 3), new Angle(5, 20), new Angle(1, 20));
            Rotate rotateCommand = new Rotate(ship);
            rotateCommand.Execute();
            Angle newAngle = ship.GetAngle();
            Assert.That(newAngle, Is.EqualTo(new Angle(6, 20)));
        }

        [Test]
        public void IncorrectRotate_AngleUndefined()
        {
            SpaceShip ship = new SpaceShip(null, new Vector(1, 3), null, new Angle(1, 20));
            Rotate rotateCommand = new Rotate(ship);
            Assert.Throws<NullReferenceException>(() => rotateCommand.Execute());
        }

        [Test]
        public void IncorrectRotate_AngularVelocityUndefined()
        {
            SpaceShip ship = new SpaceShip(null, new Vector(1, 3), new Angle(1, 20), null);
            Rotate rotateCommand = new Rotate(ship);
            Assert.Throws<NullReferenceException>(() => rotateCommand.Execute());
        }
    }
}
