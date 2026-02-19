using System;
using Game.Configs.Spawner;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public abstract class Spawner
    {
        public Action<TeamType> OnSpawnRequest;
        protected SpawnerConfig _config;
        protected float _spawnCooldown;
        protected float _spawnTime;
        protected Ship _ship;

        protected Spawner(SpawnerConfig config, Ship ship)
        {
            _config = config;
            _ship = ship;
            ResetSpawnCooldown(); 
        }

        protected abstract void Spawn(TeamType type);
        protected abstract void Despawn();
        
        protected void ResetSpawnCooldown() //можно сделать виртуальным или абстрактным в зависимости от условий
        {
            _spawnCooldown = Random.Range(_config.MinSpawnCooldown, _config.MaxSpawnCooldown);
            _spawnTime = Time.fixedTime;
        }
    }
    
    public class ShipSpawner : Spawner //Добавить точки спавна врагов, игрока и пуль
    {
        public ShipSpawner(SpawnerConfig config, Ship ship) : base(config, ship)
        {
            _ship.OnDead += Despawn;
        }

        protected override void Spawn(TeamType type)
        {
            float time = Time.fixedTime;
            
            if (time - _spawnTime < _spawnCooldown)
                return;
            
            OnSpawnRequest?.Invoke(type);
            ResetSpawnCooldown();
        }

        protected override void Despawn()
        {
            throw new NotImplementedException();
        }
    }

    public class BulletSpawner : Spawner
    {
        public BulletSpawner(SpawnerConfig config, Ship ship) : base(config, ship) { }
        
        protected override void Spawn(TeamType type)
        {
            
        }

        protected override void Despawn()
        {
            throw new NotImplementedException();
        }
    }

    public interface ISpawner
    {
        
    }
}