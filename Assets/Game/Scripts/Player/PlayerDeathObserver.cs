using UnityEngine;

namespace Game
{
    public class PlayerDeathObserver : MonoBehaviour
    {
        [SerializeField] private HealthComponent _healthComponent;

        private void OnEnable() => _healthComponent.OnDead += HandleDeath;

        private void OnDisable() => _healthComponent.OnDead -= HandleDeath;

        private void HandleDeath() => gameObject.SetActive(false);
    }
}
