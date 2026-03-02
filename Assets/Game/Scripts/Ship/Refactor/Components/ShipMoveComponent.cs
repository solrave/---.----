using UnityEngine;

namespace Game
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Vector2 _direction;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private float _smoothing = 10f; // скорость интерполяции

        private Vector2 _currentVelocity;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 targetVelocity = _direction * _speed;

            // Если сглаживание = 0, мгновенное ускорение
            if (_smoothing <= 0f)
            {
                _currentVelocity = targetVelocity;
            }
            else
            {
                _currentVelocity = Vector2.Lerp(_currentVelocity, targetVelocity, _smoothing * Time.deltaTime);
            }

            if (_currentVelocity.magnitude > 0.01f)
            {
                Vector2 newPosition = _rigidbody.position + _currentVelocity * Time.deltaTime;
                _rigidbody.MovePosition(newPosition);
            }
        }

        public void SetDirection(Vector2 dir) => _direction = dir;
    }
}