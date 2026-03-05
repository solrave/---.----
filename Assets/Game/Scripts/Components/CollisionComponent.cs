using System;
using UnityEngine;

namespace Game
{
    public class CollisionComponent : MonoBehaviour
    {
        public event Action<Collider2D> OnHit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnHit?.Invoke(other);
        }
    }
}