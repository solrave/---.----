using UnityEngine;

namespace Game
{
    public class BulletFactory : IBulletFactory
    {
        private readonly IDataProvider _dataProvider;
        private Transform _container;
        
        public BulletFactory(IDataProvider dataProvider, Transform container)
        {
            _dataProvider = dataProvider;
            _container = container;
        }

        public Bullet Get(TeamType type)
        {
            var prefab = _dataProvider.GetBulletPrefab();
            var coreConfig = _dataProvider.GetBulletCoreConfig(type);
            var visualConfig = _dataProvider.GetBulletVisualConfig(type);
            var bullet = GameObject.Instantiate(prefab, _container);
            bullet.Init(coreConfig,visualConfig);
            bullet.gameObject.SetActive(false);
            return bullet;
        }
    }
}