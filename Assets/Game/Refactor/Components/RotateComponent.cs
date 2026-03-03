using UnityEngine;
using DG.Tweening;

namespace Game
{
    public class RotateComponent : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _tiltAngle = 30f;
        [SerializeField] private float _duration = 0.3f;

        private Tween _currentTween;

        public void RotateToDirection(Vector2 direction)
        {
            float targetAngle = direction.sqrMagnitude < 0.01f ? 0f : _tiltAngle * direction.x;

            _currentTween?.Kill();
            _currentTween = _transform.DORotate(new Vector3(0, -targetAngle, 0), _duration).SetEase(Ease.OutCubic);
        }
    }
}