using UnityEngine;

namespace Game
{
    public class EnemyDeathObserver : MonoBehaviour
    {
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private Enemy _enemy;

        private void OnEnable() => _healthComponent.OnDead += HandleDeath;

        private void OnDisable() => _healthComponent.OnDead -= HandleDeath;

        private void HandleDeath() => _enemy.Despawn();
    }
}
