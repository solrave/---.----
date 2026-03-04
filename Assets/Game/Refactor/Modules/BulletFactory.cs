using Game;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    #region Instance

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

    #endregion

    [SerializeField] private GameObject _bulletPrefab;

    private PrefabPool _prefabPool;

    private void Start() => _prefabPool = new PrefabPool();

    public Bullet SpawnBullet(Vector3 position, Quaternion rotation, Vector2 direction)
    {
        var bullet = _prefabPool.Rent<Bullet>(_bulletPrefab);

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