using GameServer.Interfaces;

namespace GameServer.Models
{
    public class SpaceShip : IMovingObject, IRotatingObject
    {
        public Point Location { get; set; }
        public Vector Velocity { get; }
        public Angle Angle { get; set; }
        public Angle AngularVelocity { get; set; }

        public SpaceShip(Point location, Vector velocity, Angle angle, Angle angularVelocity)
        {
            Location = location;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
        }

        public void SetLocation(Point newLocation)
        {
            Location = newLocation;
        }
    }
}
