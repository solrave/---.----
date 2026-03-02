using DG.Tweening;
using Game.Scripts;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Game
{
    public class AnimationComponent : IAnimationComponent
    {
        private Tweener _damageAnimation;
        private readonly ShipVisualConfig _visualConfig;
        private readonly MeshRenderer _renderer;
        private readonly AudioSource _audioSource;

        public AnimationComponent(ShipVisualConfig visualConfig,
            MeshRenderer renderer, AudioSource audioSource)
        {
            _visualConfig = visualConfig;
            _renderer = renderer;
            _audioSource = audioSource;
            _renderer.material = _visualConfig.MaterialPrefab;
        }

        public void AnimateDamage()
        {
            if (_damageAnimation.IsActive())
                _damageAnimation.Kill();

            _damageAnimation = DOVirtual.Float(
                0f,
                1f,
                _visualConfig.HitDuration,
                progress => _renderer.material?.SetFloat(_visualConfig.HitPropertyName,
                    _visualConfig.HitAnimationCurve.Evaluate(progress))
            ).SetLink(_renderer.gameObject);

            if (_visualConfig.DamageSfx)
                _audioSource.PlayOneShot(_visualConfig.DamageSfx);
        }

        public void PlayFireSound()
        {
            if (_visualConfig.FireSfx)
                _audioSource.PlayOneShot(_visualConfig.FireSfx);
        }

        public void PlayFireVisual()
        {
            if (_visualConfig.FireVfx)
                _visualConfig.FireVfx.Play();
        }

        public void PlayDestroyEffectFor(Transform transform)
        {
            ParticleSystem prefab = _visualConfig.DestroyEffectPrefab;
            GameObject.Instantiate(prefab, transform.position, prefab.transform.rotation);
        }
    }

    public interface IAnimationComponent
    {
        void AnimateDamage();
        void PlayDestroyEffectFor(Transform transform);
        void PlayFireSound();
        void PlayFireVisual();
    }
}