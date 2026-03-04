using System;
using UnityEngine;

namespace Game
{
    public class HealthComponent : MonoBehaviour
    {
        public event Action<int> OnHealthChanged;
        public event Action<int> OnHealthIncreased;
        public event Action<int> OnHealthDecreased;

        [SerializeField] private int _health = 100;

        public int CurrentHealth => _currentHealthHealth;
        public int Max => _maxHealth;

        //Показывает значение в диапазоне от 0 до 1 
        //Если нужно значение от 0 до 100 - _maxHealth * 100 😇
        public float HealthPercent => _maxHealth > 0 ? (float)_currentHealthHealth / _maxHealth : 0f;

        private int _currentHealthHealth;
        private int _maxHealth;

        private void Start() => _currentHealthHealth = _maxHealth = _health;

        public void TakeDamage(int damage)
        {
            if (damage < 0) return;

            int previousHealth = _currentHealthHealth;
            _currentHealthHealth = Mathf.Max(0, _currentHealthHealth - damage);

            if (_currentHealthHealth != previousHealth)
            {
                OnHealthChanged?.Invoke(_currentHealthHealth);
                OnHealthDecreased?.Invoke(_currentHealthHealth);
            }
        }

        public void Heal(int amount)
        {
            if (amount < 0) return;

            int previousHealth = _currentHealthHealth;
            _currentHealthHealth = Mathf.Min(_maxHealth, _currentHealthHealth + amount);

            if (_currentHealthHealth != previousHealth)
            {
                OnHealthChanged?.Invoke(_currentHealthHealth);
                OnHealthIncreased?.Invoke(_currentHealthHealth);
            }
        }
    }
}