using UnityEngine;

namespace Game
{
    public class EnemyMoveController : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _stoppingDistance = 0.25f;

        private Vector2 _destination;
        private Transform _target;

        [SerializeField] private float _facingThreshold = 5f;

        public bool IsReached { get; private set; }
        public bool IsFacingTarget { get; private set; }

        public void SetDestination(Vector2 destination)
        {
            _destination = destination;
            IsReached = false;
        }

        public void SetTarget(Transform target) => _target = target;

        private void Update()
        {
            Vector2 position = _rigidbody.position;
            Vector2 toDestination = _destination - position;
            IsReached = toDestination.sqrMagnitude <= _stoppingDistance * _stoppingDistance;

            _enemy.SetDirection(IsReached ? Vector2.zero : toDestination.normalized);

            Vector2 lookDirection = IsReached && _target != null
                ? (Vector2)_target.position - position
                : toDestination;

            if (lookDirection.sqrMagnitude > 0.001f)
            {
                float targetAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
                _rigidbody.MoveRotation(targetAngle);

                float delta = Mathf.DeltaAngle(_rigidbody.rotation, targetAngle);
                IsFacingTarget = Mathf.Abs(delta) <= _facingThreshold;
            }
        }
    }
}
