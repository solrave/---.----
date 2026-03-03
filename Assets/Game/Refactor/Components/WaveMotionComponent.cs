using UnityEngine;
using DG.Tweening;

namespace Game
{
    public class WaveMotionComponent : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _waveHeight = 0.5f;
        [SerializeField] private float _waveDuration = 2f;

        private void Start()
        {
            // Вверх-вниз по оси Z
            _transform.DOLocalMoveZ(_waveHeight, _waveDuration / 2f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy() => _transform.DOKill();
    }
}