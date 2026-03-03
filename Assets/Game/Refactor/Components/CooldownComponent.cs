using System;
using UnityEngine;

namespace Game
{
    public class CooldownComponent : MonoBehaviour
    {
        public event Action OnExpired;

        [SerializeField] private float _duration;

        public bool IsExpired => _current <= 0;
        private float _current;

        private void Update()
        {
            _current = Mathf.Max(0, _current - Time.deltaTime);

            if (_current <= 0)
                OnExpired?.Invoke();
        }

        public void Reset() => _current = _duration;
    }
}