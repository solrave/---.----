using System;
using UnityEngine;

namespace Game
{
    public class Cooldown : MonoBehaviour
    {
        [SerializeField] private float _current;
        [SerializeField] private float _duration;

        public Cooldown(float duration, float current = 0)
        {
            _duration = duration;
            _current = current;
        }

        public float Duration => _duration;

        public bool IsExpired()
        {
            return _current <= 0;
        }

        public float GetProgress()
        {
            return _current / _duration;
        }

        public void Reset()
        {
            _current = _duration;
        }

        public void Update()
        {
            _current = Mathf.Max(0, _current - Time.deltaTime);
        }

        public void SetDuration(float duration)
        {
            _duration = duration;
        }
    }
}