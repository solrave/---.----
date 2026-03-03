using System;
using UnityEngine;

namespace Game
{
    public class CooldownComponent : MonoBehaviour
    {
        public event Action OnExpired;

        [SerializeField] private float _duration;
        [SerializeField] private bool _autoStart = true;

        private float _current;
        private bool _isRunning;

        private void Start()
        {
            if (_autoStart)
                Reset();
        }

        private void Update()
        {
            if (!_isRunning) return;

            _current = Mathf.Max(0, _current - Time.deltaTime);

            if (_current <= 0)
            {
                _isRunning = false;
                OnExpired?.Invoke();
            }
        }

        public bool IsExpired() => _current <= 0;

        public void Reset()
        {
            _current = _duration;
            _isRunning = true;
        }
    }
}