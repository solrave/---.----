using System;
using UnityEngine;

namespace Game //R
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BulletConfig _bulletViewConfig;
        [SerializeField] private Bullet _playerBulletPrefab;
        [SerializeField] private Bullet _enemyBulletPrefab;
        private BulletPool _enemyBulletPool;
        private BulletPool _playerBulletPool;

        private void Awake()
        {
            _enemyBulletPool = new BulletPool(_bulletViewConfig);
        }
    }
}