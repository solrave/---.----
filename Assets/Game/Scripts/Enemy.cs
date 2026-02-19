using UnityEngine;

namespace Game
{
    // +
    public sealed class Enemy : Ship
    {
        
        private Ship _target;
        private Vector2 _destination;
        private float _fireTime;

        [SerializeField]
        private float _stoppingDistance = 0.25f;

        private IEnemyDespawner _despawner;

        public void SetDestination(Vector2 destination) => _destination = destination;
        
        public void SetHealth(int health) => _currentHealth = health;

        public void SetDespawner(IEnemyDespawner despawner) => _despawner = despawner;

        private void OnEnable() => this.OnDead += this.OnCharacterDead;

        private void OnDisable() => this.OnDead -= this.OnCharacterDead;

        private void OnCharacterDead() => _despawner.Despawn(this);

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (this._currentHealth <= 0 || this._target == null || this._target.CurrentHealth <= 0)
                return;

            Vector2 distance = _destination - (Vector2) this.transform.position;
            bool isNotReached = distance.sqrMagnitude > _stoppingDistance * _stoppingDistance;
            
            _moveDirection = isNotReached ? distance.normalized : Vector3.zero;

            if (isNotReached)
            {
                _moveComponent.SetDirection(distance.normalized);
            }
            else
            {
                float time = Time.time;
                if (time - _fireTime >= _coreConfig.FireCooldown)
                {
                    this.Fire();
                    _fireTime = time;
                }
            }
        }
    }
}