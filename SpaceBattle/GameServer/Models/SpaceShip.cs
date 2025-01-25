using GameServer.Enums;
using GameServer.Interfaces;

namespace GameServer.Models
{
    public class SpaceShip : IMovingObject, IRotatingObject
    {
        private Point _location { get; set; }
        private Vector _velocity { get; set; }
        private Position _position { get; set; }

        public SpaceShip(Point location, Vector velocity, Position position)
        {
            _location = location;
            _velocity = velocity;
            _position = position;
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

        public Position GetPosition()
        {
            return _position;
        }

        public void SetPosition(Position newPosition)
        {
            _position = newPosition;
        }
    }
}
