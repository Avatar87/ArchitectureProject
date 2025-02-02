using GameServer.Interfaces;

namespace GameServer.Models
{
    public class SpaceShip : IMovingObject, IRotatingObject
    {
        private Point _location { get; set; }
        private Vector _velocity { get; set; }
        private Angle _angle { get; set; }
        private Angle _angularVelocity { get; set; }

        public SpaceShip(Point location, Vector velocity, Angle angle, Angle angularVelocity)
        {
            _location = location;
            _velocity = velocity;
            _angle = angle;
            _angularVelocity = angularVelocity;
        }

        public Point GetLocation()
        {
            return _location;
        }
        public void SetLocation(Point newLocation)
        {
            _location = newLocation;
        }

        public Vector GetVelocity()
        {
            return _velocity;
        }

        public Angle GetAngle()
        {
            return _angle;
        }
        public void SetAngle(Angle angle)
        {
            _angle = angle;
        }
        public Angle GetAngularVelocity()
        {
            return _angularVelocity;
        }
    }
}
