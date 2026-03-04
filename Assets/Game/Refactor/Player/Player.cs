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

        public void Fire()
        {
            if (!_fireRate.IsExpired) return;
            
            _fireComponent.Fire();
            _fireRate.Reset();
        }

        private void FixedUpdate()
        {
            if (_healthComponent.CurrentHealth <= 0) return;

            _moveComponent.Move(_direction);
            _tiltComponent.TiltToDirection(_direction);
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
    }
}