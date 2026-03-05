using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private FireComponent _fireComponent;
        [SerializeField] private CooldownComponent _fireRate;
        [SerializeField] private TiltComponent _tiltComponent;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private PlayerSettings _playerSettings;

        private Vector2 _direction;

        private void Awake()
        {
            _moveComponent.AddCondition(() => _healthComponent.IsAlive);
            _fireComponent.AddCondition(() => _healthComponent.IsAlive);
            _fireComponent.AddCondition(() => _fireRate.IsExpired);
        }

        private void OnEnable() => _fireComponent.OnFire += OnFired;

        private void OnDisable() => _fireComponent.OnFire -= OnFired;

        private void FixedUpdate()
        {
            _moveComponent.Move(_direction);
            _tiltComponent.TiltToDirection(_direction);
        }

        public void SetDirection(Vector2 direction) => _direction = direction;

        public void Fire() => _fireComponent.Fire();

        private void OnFired() => _fireRate.Reset();
    }
}
