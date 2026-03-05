using System;
using UnityEngine;

namespace Game
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private bool _useSmoothing = true;
        [SerializeField] private float _smoothing = 10f;

        private readonly AndCondition _andCondition = new();
        private Vector2 _currentVelocity;

        public void AddCondition(Func<bool> condition) => _andCondition.AddCondition(condition);

        public void Move(Vector2 direction)
        {
            if (!_andCondition.IsTrue())
            {
                _currentVelocity = Vector2.zero;
                return;
            }

            if (direction.sqrMagnitude < 0.001f)
            {
                _currentVelocity = Vector2.zero;
                return;
            }

            Vector2 targetVelocity = direction * _speed;

            _currentVelocity = _useSmoothing
                ? Vector2.Lerp(_currentVelocity, targetVelocity, _smoothing * Time.deltaTime)
                : targetVelocity;

            _rigidbody.MovePosition(_rigidbody.position + _currentVelocity * Time.deltaTime);
        }
    }
}
