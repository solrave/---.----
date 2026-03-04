using UnityEngine;

namespace Game
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private bool _useSmoothing = true;
        [SerializeField] private float _smoothing = 10f;

        private Vector2 _currentVelocity;

        public void Move(Vector2 direction)
        {
            Vector2 targetVelocity = direction * _speed;

            _currentVelocity = _useSmoothing
                ? Vector2.Lerp(_currentVelocity, targetVelocity, _smoothing * Time.deltaTime)
                : targetVelocity;

            _rigidbody.MovePosition(_rigidbody.position + _currentVelocity * Time.deltaTime);
        }
    }
}