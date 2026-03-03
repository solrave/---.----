using UnityEngine;

namespace Game
{
    public class BulletFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;

        private PrefabPool _prefabPool;

        private void Start()
        {
            _prefabPool = new PrefabPool();
        }

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            var bullet = _prefabPool.Spawn<CollisionComponent>(_bulletPrefab);
            bullet.transform.SetPositionAndRotation(position, rotation);
            bullet.GetComponent<CooldownComponent>().Reset();
            bullet.GetComponent<BulletDespawnObserver>().OnDespawn += Despawn;
        }

        public void Despawn(GameObject bullet)
        {
            bullet.GetComponent<BulletDespawnObserver>().OnDespawn -= Despawn;
            _prefabPool.DeSpawn(bullet);
        }
    }
}