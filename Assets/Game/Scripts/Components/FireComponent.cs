using System;
using UnityEngine;

namespace Game
{
    public class FireComponent : MonoBehaviour
    {
        public event Action OnFire;

        [SerializeField] private Transform _firePoint;
        [SerializeField] private GameObject _bulletPrefab;

        private readonly AndCondition _andCondition = new();
        private BulletFactory _bulletFactory;

        private void Start() => _bulletFactory = BulletFactory.Instance;

        public void AddCondition(Func<bool> condition) => _andCondition.AddCondition(condition);

        public void Fire()
        {
            if (!_andCondition.IsTrue()) return;

            var direction = _firePoint.up;
            var rotation = Quaternion.LookRotation(direction);

            _bulletFactory.SpawnBullet(_bulletPrefab, _firePoint.position, rotation, direction);

            OnFire?.Invoke();
        }
    }
}
