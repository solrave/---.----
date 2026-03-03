using System;
using UnityEngine;

namespace Game
{
    public class BulletDespawnObserver : MonoBehaviour
    {
        public event Action<GameObject> OnDespawn;

        [SerializeField] private CollisionComponent _collisionComponent;
        [SerializeField] private CooldownComponent _lifeTime;

        private BulletFactory _factory;

        private void OnEnable()
        {
            _collisionComponent.OnHit += Despawn;
            _lifeTime.OnExpired += Despawn;
        }

        private void OnDisable()
        {
            _collisionComponent.OnHit -= Despawn;
            _lifeTime.OnExpired -= Despawn;
        }

        private void Despawn() => OnDespawn?.Invoke(gameObject);

        private void Despawn(Collider2D _) => OnDespawn?.Invoke(gameObject);
    }
}