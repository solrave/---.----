using System;
using UnityEngine;

namespace Game
{
    // +
    
    public sealed class MoveComponent : IMoveComponent
    {
        public event Action<Vector3> OnMoved;
        public Vector2? Direction => _direction;
        
        private Rigidbody2D _rigidbody;
        
        private float _speed;
        private Vector2? _direction;

        public void SetSpeed(float speed) => _speed = speed;

        public void SetDirection(Vector2 direction) => _direction = direction;

        public MoveComponent(Rigidbody2D rigidbody, float speed)
        {
            _rigidbody = rigidbody;
            _speed = speed;
        }

        public void Move()
        {
            if (!_direction.HasValue)
                return;

            Vector2 direction = _direction.Value;
            Vector2 newPosition = _rigidbody.position + direction * (_speed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPosition);
            _direction = null;
            
            this.OnMoved?.Invoke(direction);
        }
    }

    public interface IMoveComponent
    {
        Vector2? Direction { get; }
        void SetSpeed(float speed);
        void SetDirection(Vector2 direction);
        void Move();
    }
}