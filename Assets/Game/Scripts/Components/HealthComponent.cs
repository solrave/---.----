using System;
using UnityEngine;

namespace Game
{
    public class HealthComponent : MonoBehaviour
    {
        public event Action<int> OnHealthChanged;
        public event Action<int> OnHealthIncreased;
        public event Action<int> OnHealthDecreased;
        public event Action OnDead;

        [SerializeField] private int _health = 100;

        public int CurrentHealth => _currentHealth;
        public int Max => _maxHealth;
        public float HealthPercent => _maxHealth > 0 ? (float)_currentHealth / _maxHealth : 0f;
        public bool IsAlive => _currentHealth > 0;

        private int _currentHealth;
        private int _maxHealth;

        private void Start() => ResetToMax();

        public void ResetToMax() => _currentHealth = _maxHealth = _health;

        public void TakeDamage(int damage)
        {
            if (damage < 0) return;

            int previousHealth = _currentHealth;
            _currentHealth = Mathf.Max(0, _currentHealth - damage);

            if (_currentHealth != previousHealth)
            {
                OnHealthChanged?.Invoke(_currentHealth);
                OnHealthDecreased?.Invoke(_currentHealth);

                if (_currentHealth <= 0)
                    OnDead?.Invoke();
            }
        }

        public void Heal(int amount)
        {
            if (amount < 0) return;

            int previousHealth = _currentHealth;
            _currentHealth = Mathf.Min(_maxHealth, _currentHealth + amount);

            if (_currentHealth != previousHealth)
            {
                OnHealthChanged?.Invoke(_currentHealth);
                OnHealthIncreased?.Invoke(_currentHealth);
            }
        }
    }
}
