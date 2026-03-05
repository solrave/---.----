using UnityEngine;

namespace Game
{
    public class BulletDamageObserver : MonoBehaviour
    {
        [SerializeField] private CollisionComponent _collisionComponent;
        [SerializeField] private Bullet _bullet;

        private void OnEnable() => _collisionComponent.OnHit += HandleHit;

        private void OnDisable() => _collisionComponent.OnHit -= HandleHit;

        private void HandleHit(Collider2D other)
        {
            if (other.TryGetComponent(out HealthComponent health))
                health.TakeDamage(_bullet.Damage);
        }
    }
}
