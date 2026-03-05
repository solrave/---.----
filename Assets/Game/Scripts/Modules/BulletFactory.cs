using UnityEngine;

namespace Game
{
    public class BulletFactory : MonoBehaviour
    {
        public static BulletFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Instantiate(Resources.Load<BulletFactory>("Prefabs/BulletFactory"));

                return _instance;
            }
        }

        private static BulletFactory _instance;

        private PrefabPool _prefabPool;

        private void Start() => _prefabPool = new PrefabPool();

        public Bullet SpawnBullet(GameObject prefab, Vector3 position, Quaternion rotation, Vector2 direction)
        {
            var bullet = _prefabPool.Rent<Bullet>(prefab);

            bullet.SetPositionAndRotation(position, rotation);
            bullet.ResetLifeTime();
            bullet.SetDirection(direction);
            bullet.OnDespawn += Despawn;

            return bullet;
        }

        private void Despawn(Bullet bullet)
        {
            bullet.OnDespawn -= Despawn;
            _prefabPool.Return(bullet.gameObject);
        }
    }
}
