using System.Collections;
using System.Collections.Generic;
using Modules.UI;
using Modules.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    // +
    public sealed class EnemyOrchestrator : MonoBehaviour, IEnemyDespawner
    {
        
        
        [Header("Pool")]
        [SerializeField]
        private Enemy _prefab;

        [SerializeField]
        private Transform _container;
        
        private readonly Queue<Enemy> _pool = new();

        [Header("Target")]
        [SerializeField]
        private Ship _player;
        
        [Header("Points")]
        [SerializeField]
        private Transform[] _spawnPositions;
        
        [SerializeField]
        private Transform[] _attackPositions;
        
        private int _spawnIndex;
        private int _attackIndex;
        
        [Header("Bullets")]
        [SerializeField]
        private BulletWorldGO _bulletWorld;
        
        [Header("UI")]
        [SerializeField]
        private ScoreView _scoreView;
        
        private int _destroyedEnemies;
        
        private void Awake()
        {
            _spawnPositions.Shuffle();
            _attackPositions.Shuffle();
            _scoreView.SetValue(_destroyedEnemies);
        }
        
        private void Start()
        {
            this.ResetSpawnCooldown();
        }

        private void FixedUpdate()
        {
            
            
            if (_pool.TryDequeue(out Enemy enemy))
                enemy.gameObject.SetActive(true);
            else
                enemy = Instantiate(_prefab, _container);

            enemy.transform.position = this.NextSpawnPosition();
            enemy.SetDestination(this.NextDestination());
            enemy.SetHealth(enemy._coreConfig.Health);

            enemy.target = _player;
            enemy.SetDespawner(this);
            enemy.OnFire += this.OnFire;
                
            this.ResetSpawnCooldown();
        }

        public void Despawn(Enemy enemy)
        {
            _destroyedEnemies++;
            _scoreView.SetValue(_destroyedEnemies);
            this.StartCoroutine(DespawnInNextFrame(enemy));
        }

        private IEnumerator DespawnInNextFrame(Enemy enemy)
        {
            yield return null;
            enemy.gameObject.SetActive(false);
            _pool.Enqueue(enemy);
        }
        
        private void OnFire(Ship enemy)
        {
            Vector2 position = enemy.firePoint.position;
            Vector2 target = _player.transform.position;
            Vector2 direction = (target - position).normalized;
            _bulletWorld.Spawn(
                enemy.firePoint.position,
                direction,
                enemy.bulletSpeed,
                enemy.bulletDamage,
                TeamType.Enemy
            );
        }
        
        private Vector3 NextSpawnPosition()
        {
            if (_spawnIndex >= _spawnPositions.Length)
            {
                _spawnPositions.Shuffle();
                _spawnIndex = 0;
            }

            return _spawnPositions[_spawnIndex++].position;
        }

        private Vector3 NextDestination()
        {
            if (_attackIndex >= _attackPositions.Length)
            {
                _attackPositions.Shuffle();
                _attackIndex = 0;
            }

            return _attackPositions[_attackIndex++].position;
        }
    }
}