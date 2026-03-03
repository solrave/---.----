using System;
using UnityEngine;

namespace Game
{
    public class FireComponent : MonoBehaviour
    {
        public event Action OnFire;

        [SerializeField] private Transform _firePoint;

        private BulletFactory _bulletFactory;

        private void Start() => _bulletFactory = BulletFactory.Instance;

        public void Fire()
        {
            var direction = _firePoint.up;
            var rotation = Quaternion.LookRotation(direction);
            
            _bulletFactory.SpawnBullet(_firePoint.position, rotation, direction);

            OnFire?.Invoke();
        }
    }
}