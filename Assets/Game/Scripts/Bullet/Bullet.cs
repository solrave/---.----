using System;
using UnityEngine;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnDespawn;

        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private CollisionComponent _collisionComponent;
        [SerializeField] private CooldownComponent _lifeTime;
        [SerializeField] private int _damage = 1;

        public int Damage => _damage;

        private Vector2 _moveDirection;

        private void FixedUpdate()
        {
            _moveComponent.Move(_moveDirection);
        }

        public void ResetLifeTime() => _lifeTime.Reset();

        public void SetDirection(Vector2 direction) => _moveDirection = direction;

        public void Despawn() => OnDespawn?.Invoke(this);

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation) =>
            transform.SetPositionAndRotation(position, rotation);
    }
}
