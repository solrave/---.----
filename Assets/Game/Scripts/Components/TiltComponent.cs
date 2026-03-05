using UnityEngine;
using DG.Tweening;

namespace Game
{
    public class TiltComponent : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _tiltAngleX = 15f;
        [SerializeField] private float _tiltAngleY = 30f;
        [SerializeField] private float _duration = 0.3f;

        private Tween _currentTween;

        public void TiltToDirection(Vector2 direction)
        {
            float targetAngleX = direction.sqrMagnitude < 0.01f ? 0f : _tiltAngleX * direction.y;
            float targetAngleY = direction.sqrMagnitude < 0.01f ? 0f : _tiltAngleY * direction.x;

            _currentTween?.Kill();
            _currentTween = _transform.DOLocalRotate(new Vector3(targetAngleX, -targetAngleY, 0), _duration).SetEase(Ease.OutCubic);
        }
    }
}