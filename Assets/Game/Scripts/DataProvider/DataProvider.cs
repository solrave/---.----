using System;
using System.Collections.Generic;
using Game.Configs.Spawner;
using Game.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{//R
    public class DataProvider : MonoBehaviour, IDataProvider
    {
        [Header("Prefabs")]
        
        [SerializeField] 
        private Bullet _bulletPrefab;
        
        [SerializeField] 
        private Ship _shipPrefab;
        
        [Header("BulletConfigs")]
        
        [SerializeField] 
        private List<BulletCoreConfig> _bulletCoreConfigs;
        
        [SerializeField]
        private List<BulletVisualConfig> _bulletVisualConfigs;
        
        [Header("ShipConfigs")]
        
        [SerializeField]
        private List<ShipCoreConfig> _shipCoreConfigs;
        
        [SerializeField] 
        private List<ShipVisualConfig> _shipVisualConfigs;

        [Header("SpawnerConfigs")]
        [SerializeField] private List<SpawnerConfig> _spawnerConfigs;
        
        
        private Dictionary<TeamType, BulletCoreConfig> _bulletCoreConfigStorage;
        private Dictionary<TeamType, BulletVisualConfig> _bulletVisualConfigStorage;
        
        private Dictionary<TeamType, ShipCoreConfig> _shipCoreConfigStorage;
        private Dictionary<TeamType, ShipVisualConfig> _shipVisualConfigStorage;
        
        private Dictionary<SpawnerType, SpawnerConfig> _spawnerConfigStorage;
        
        private void Awake()
        {
            UploadConfigs();
        }
        
        public Bullet GetBulletPrefab() =>  _bulletPrefab;
        public Ship GetShipPrefab() =>  _shipPrefab;
        
        public SpawnerConfig GetSpawnerConfig(SpawnerType type)
        {
            return _spawnerConfigStorage[type];
        }

        public BulletCoreConfig GetBulletCoreConfig(TeamType teamType)
        {
            return _bulletCoreConfigStorage[teamType];
        }

        public BulletVisualConfig GetBulletVisualConfig(TeamType teamType)
        {
            return _bulletVisualConfigStorage[teamType];
        }

        public ShipCoreConfig GetShipCoreConfig(TeamType teamType)
        {
            return _shipCoreConfigStorage[teamType];
        }

        public ShipVisualConfig GetShipVisualConfig(TeamType teamType)
        {
            return _shipVisualConfigStorage[teamType];
        }

        private void UploadConfigs()
        {
            UploadBulletCoreConfigs();
            
            UploadBulletVisualConfigs();
            
            UploadShipCoreConfigs();
            
            UploadShipVisualConfigs();
        }

        private void UploadShipVisualConfigs()
        {
            foreach (ShipVisualConfig config in _shipVisualConfigs)
            {
                _shipVisualConfigStorage.Add(config.Team, config);
            }
        }

        private void UploadShipCoreConfigs()
        {
            foreach (ShipCoreConfig config in _shipCoreConfigs)
            {
                _shipCoreConfigStorage.Add(config.Team, config);
            }
        }

        private void UploadBulletVisualConfigs()
        {
            foreach (BulletVisualConfig config in _bulletVisualConfigs)
            {
                _bulletVisualConfigStorage.Add(config.Team, config);
            }
        }

        private void UploadBulletCoreConfigs()
        {
            foreach (BulletCoreConfig config in _bulletCoreConfigs)
            {
                _bulletCoreConfigStorage.Add(config.Team, config);
            }
        }
        
        private void UploadSpawnerConfigs()
        {
            foreach (SpawnerConfig config in _spawnerConfigs)
            {
                _spawnerConfigStorage.Add(config.Type, config);
            }
        }
    }
}