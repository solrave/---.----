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
        private bool _firedExpired = true;

        private void Update()
        {
            if (_current <= 0) return;

            _current = Mathf.Max(0, _current - Time.deltaTime);

            if (_current <= 0 && !_firedExpired)
            {
                _firedExpired = true;
                OnExpired?.Invoke();
            }
        }

        public void Reset()
        {
            _current = _duration;
            _firedExpired = false;
        }
    }
}
