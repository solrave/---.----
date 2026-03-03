using System;
using UnityEngine;

namespace Game
{
    public class FireComponent : MonoBehaviour
    {
        public event Action OnFire;

        [SerializeField] private BulletFactory _bulletFactory;
        [SerializeField] private Transform _firePoint;

        public void Fire()
        {
            _bulletFactory.Spawn(_firePoint.position, Quaternion.identity);
            OnFire?.Invoke();
        }
    }
}