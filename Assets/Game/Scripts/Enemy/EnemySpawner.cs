using UnityEngine;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;
        [SerializeField] private CooldownComponent _spawnCooldown;
        [SerializeField] private Transform _playerTarget;

        private PrefabPool _pool;
        private int _spawnIndex;
        private int _attackIndex;

        private void Awake() => _pool = new PrefabPool();

        private void OnEnable() => _spawnCooldown.OnExpired += SpawnEnemy;

        private void OnDisable() => _spawnCooldown.OnExpired -= SpawnEnemy;

        private void Start() => _spawnCooldown.Reset();

        private void SpawnEnemy()
        {
            var enemy = _pool.Rent<Enemy>(_enemyPrefab);

            enemy.transform.position = NextSpawnPosition();
            enemy.SetDestination(NextAttackPosition());
            enemy.SetTarget(_playerTarget);
            enemy.ResetState();
            enemy.OnDespawn += Despawn;

            _spawnCooldown.Reset();
        }

        private void Despawn(Enemy enemy)
        {
            enemy.OnDespawn -= Despawn;
            _pool.Return(enemy.gameObject);
        }

        private Vector3 NextSpawnPosition()
        {
            if (_spawnIndex >= _spawnPositions.Length)
                _spawnIndex = 0;

            return _spawnPositions[_spawnIndex++].position;
        }

        private Vector3 NextAttackPosition()
        {
            if (_attackIndex >= _attackPositions.Length)
                _attackIndex = 0;

            return _attackPositions[_attackIndex++].position;
        }
    }
}
