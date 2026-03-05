using System;
using UnityEngine;

namespace Game
{
    public class BulletDestroyObserver : MonoBehaviour
    {
        [SerializeField] private CollisionComponent _collisionComponent;
        [SerializeField] private CooldownComponent _lifeTime;
        [SerializeField] private Bullet _bullet;

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

        private void Despawn() => _bullet.Despawn();

        private void Despawn(Collider2D _) => _bullet.Despawn();
    }
}