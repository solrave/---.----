// using System;
// using Game.Configs.Spawner;
// using UnityEngine;
// using Random = UnityEngine.Random;
//
// namespace Game
// {
//     public abstract class Spawner
//     {
//         public Action<TeamType> OnSpawnRequest;
//         private readonly SpawnerConfig _config;
//         protected float spawnCooldown;
//         protected float spawnTime;
//         protected PoolManager pool;
//
//         protected Spawner(SpawnerConfig config, PoolManager pool)
//         {
//             this._config = config;
//             this.pool = pool;
//             ResetSpawnCooldown(); 
//         }
//
//         protected abstract void Spawn(TeamType type);
//         protected abstract void Despawn();
//         
//         protected void ResetSpawnCooldown() //можно сделать виртуальным или абстрактным в зависимости от условий
//         {
//             spawnCooldown = Random.Range(_config.MinSpawnCooldown, _config.MaxSpawnCooldown);
//             spawnTime = Time.fixedTime;
//         }
//     }
// }