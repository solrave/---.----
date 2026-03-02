// using System;
// using UnityEngine;
// using Random = UnityEngine.Random;
//
// namespace Game
// {
//     public class ShipSpawner //Добавить точки спавна врагов, игрока и пуль
//     {
//         private readonly PoolManager _pool;
//         private readonly Transform _playerStartPosition;
//         private readonly Transform[] _enemySpawnPoints;
//         private readonly float _spawnCooldown = Random.Range(2f, 3f);
//         private float _spawnTime;
//
//         public ShipSpawner(PoolManager pool, Transform playerStartPosition, Transform[] enemySpawnPoints)
//         {
//             _pool = pool;
//             _playerStartPosition = playerStartPosition;
//             _enemySpawnPoints = enemySpawnPoints;
//         }
//         
//         private void Spawn(TeamType type)
//         {
//             float time = Time.fixedTime;
//             
//             if (time - _spawnTime < _spawnCooldown)
//                 return;
//             //spawn logic here
//             ResetCooldown();
//         }
//
//         private void Despawn()
//         {
//             throw new NotImplementedException();
//         }
//
//         private void ResetCooldown() 
//         {
//             _spawnTime = Time.fixedTime;
//         }
//     }
// }