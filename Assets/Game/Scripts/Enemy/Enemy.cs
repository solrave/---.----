using System;
using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnDespawn;

        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private FireComponent _fireComponent;
        [SerializeField] private CooldownComponent _fireRate;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private EnemyMoveController _moveController;

        private Vector2 _direction;

        private void Awake()
        {
            _moveComponent.AddCondition(() => _healthComponent.IsAlive);
            _fireComponent.AddCondition(() => _healthComponent.IsAlive);
            _fireComponent.AddCondition(() => _fireRate.IsExpired);
            _fireComponent.AddCondition(() => _moveController.IsFacingTarget);
        }

        private void OnEnable() => _fireComponent.OnFire += OnFired;

        private void OnDisable() => _fireComponent.OnFire -= OnFired;

        private void FixedUpdate() => _moveComponent.Move(_direction);

        public void SetDirection(Vector2 direction) => _direction = direction;

        public void Fire() => _fireComponent.Fire();

        public void SetDestination(Vector2 destination) => _moveController.SetDestination(destination);

        public void SetTarget(Transform target) => _moveController.SetTarget(target);

        public void ResetState()
        {
            _direction = Vector2.zero;
            _healthComponent.ResetToMax();
        }

        public void Despawn() => OnDespawn?.Invoke(this);

        private void OnFired() => _fireRate.Reset();
    }
}
