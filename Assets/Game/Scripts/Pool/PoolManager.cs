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
                    ? _playerBulletPool.Dequeue() //Set Active TRUE???
                    : _bulletFactory.Get(type),
                
                TeamType.Enemy => _enemyBulletPool.Count > 0
                    ? _enemyBulletPool.Dequeue() //Set Active TRUE???
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
}