using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{//R
    public class PoolManager
    {
        private IDataProvider _dataProvider; 
        private Transform _container;
        
        private IBulletFactory _bulletFactory;
        private IShipFactory _shipFactory;

        private readonly Queue<Bullet> _playerBulletPool = new();
        private readonly Queue<Bullet> _enemyBulletPool = new();
        private readonly Queue<Ship> _enemyShipPool = new();
        
        public PoolManager(IDataProvider dataProvider, Transform container)
        {
            _dataProvider = dataProvider;
            _container = container;
            _bulletFactory = new BulletFactory(_dataProvider, _container);
            _shipFactory = new ShipFactory(_dataProvider, _container);
            PopulatePools();
        }

        public Bullet GetBullet(TeamType type)
        {
            return type switch      //я пока не понял как избежать дубляжа в этих методах
            {
                TeamType.Player => _playerBulletPool.Count > 0
                    ? _playerBulletPool.Dequeue()
                    : _bulletFactory.Get(type),
                
                TeamType.Enemy => _enemyBulletPool.Count > 0
                    ? _enemyBulletPool.Dequeue()
                    : _bulletFactory.Get(type),
                
                _ => throw new InvalidOperationException($"Invalid team type! {nameof(type)} not found.")
            };
        }

        public Ship GetShip(TeamType type)
        {
            return type switch
            {
                TeamType.Player => _shipFactory.Get(type),
                
                TeamType.Enemy => _enemyShipPool.Count > 0
                    ? _enemyShipPool.Dequeue()
                    : _shipFactory.Get(type),
                
                _ => throw new InvalidOperationException($"Invalid team type! {nameof(type)} not found.")
            };
        }

        private void PopulatePools()
        {
            for (int i = 0; i < 15; i++)
            {
                _enemyBulletPool.Enqueue(_bulletFactory.Get(TeamType.Enemy));
                _playerBulletPool.Enqueue(_bulletFactory.Get(TeamType.Player));
                _enemyShipPool.Enqueue(_shipFactory.Get(TeamType.Enemy));
            }
        }
    }

    internal interface IShipFactory
    {
        Ship Get(TeamType type);
    }

    public interface IBulletFactory
    {
        Bullet Get(TeamType type);
    }

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
    
    public class ShipFactory : IShipFactory
    {
        private readonly IDataProvider _dataProvider;
        private Transform _container;
        
        public ShipFactory(IDataProvider dataProvider, Transform container)
        {
            _dataProvider = dataProvider;
            _container = container;
        }

        public Ship Get(TeamType type)
        {
            var prefab = _dataProvider.GetShipPrefab();
            var coreConfig = _dataProvider.GetShipCoreConfig(type);
            var visualConfig = _dataProvider.GetShipVisualConfig(type);
            var ship = GameObject.Instantiate(prefab, _container);
            ship.Init(coreConfig,visualConfig);
            ship.gameObject.SetActive(false);
            return ship;
        }
    }
    
}