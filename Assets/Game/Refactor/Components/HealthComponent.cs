using UnityEngine;

namespace Game
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float _health;

        public void TakeDamage(float damage)
        {
            _health -= damage;
        }
    }
}