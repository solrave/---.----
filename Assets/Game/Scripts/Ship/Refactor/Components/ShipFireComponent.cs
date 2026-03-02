using System;
using UnityEngine;

namespace Game
{
    public class ShipFireComponent : MonoBehaviour
    {
        public event Action OnFire;

        [SerializeField] private PrefabPool _prefabPool;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Cooldown _cooldown;

        public void Fire()
        {
            if (!_cooldown.IsExpired()) return;

            var bullet = _prefabPool.Spawn(_bulletPrefab);
            bullet.transform.SetPositionAndRotation(_firePoint.position, Quaternion.identity);

            bullet.transform.GetComponent<LifeTimeComponent>().Reset();
            bullet.transform.GetComponent<LifeTimeComponent>().SetPrefabPool(_prefabPool);

            OnFire?.Invoke();

            _cooldown.Reset();
        }
    }
}