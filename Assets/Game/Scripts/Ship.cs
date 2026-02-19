using System;
using DG.Tweening;
using Game.Scripts;
using UnityEngine;

namespace Game
{
    // R
    public abstract class Ship : MonoBehaviour
    {
        public event Action<int> OnHealthChanged;
        public event Action OnDead;
        public event Action<TeamType> OnFire;
        
        [SerializeField] private Transform firePoint;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private MeshRenderer _renderer;

        public int CurrentHealth => _currentHealth;
        
        //protected Vector3 _moveDirection;
        protected int _currentHealth;
        protected ShipCoreConfig _coreConfig;
        protected IMoveComponent _moveComponent;
        private ShipVisualConfig _visualConfig;
        
        private float _fireTime;
        private Transform _transform;
        private Material _material;
        private Tweener _damageAnimation;
        private AudioSource _audioSource;

        protected virtual void FixedUpdate() => _moveComponent.Move();
        protected virtual void LateUpdate() => this.AnimateMovement(Time.deltaTime);

        public void Init(ShipCoreConfig coreConfig, ShipVisualConfig visualConfig)
        {
            _coreConfig = coreConfig; //flyweight pattern
            _visualConfig = visualConfig;
            _currentHealth = _coreConfig.Health;
            _material = _visualConfig.MaterialPrefab;
            _audioSource = GetComponent<AudioSource>();
            _moveComponent = new MoveComponent(_rigidbody, _coreConfig.MoveSpeed); //rigid dependency!!
            _material = new Material(_visualConfig.MaterialPrefab);
            _renderer.material = _material;
        }

        protected void Fire()
        {
            float time = Time.time;
            if (time - _fireTime < _coreConfig.FireCooldown || this._currentHealth <= 0)
                return;
            
                //ParticleManager => play sfx/vfx
                
            if (_visualConfig.FireSfx)
                _audioSource.PlayOneShot(_visualConfig.FireSfx);

            if (_visualConfig.FireVfx)
                _visualConfig.FireVfx.Play();

            this.OnFire?.Invoke(_coreConfig.Team);
            _fireTime = time;
        }
        
        private void AnimateMovement(float deltaTime)
        {
            Vector3 shipAngles = _transform.localEulerAngles;
            shipAngles.x = _coreConfig.MoveRotationAngle * _moveComponent.Direction.Value.y;
            shipAngles.y = _coreConfig.MoveRotationAngle / 2 * _moveComponent.Direction.Value.x * -1f;
            
            Quaternion shipRotation = Quaternion.Euler(shipAngles);
            float t = _coreConfig.MoveSpeed * deltaTime;
            _transform.localRotation = Quaternion.Lerp(_transform.localRotation, shipRotation, t);
        }
        
        public void NotifyAboutHealthChanged(int health)
        {
            if (health > 0)
                this.AnimateDamage();

            this.OnHealthChanged?.Invoke(health);
        }

        public void NotifyAboutDead()
        {
            // Instantiate particle vfx 
            ParticleSystem prefab = _visualConfig.DestroyEffectPrefab;
            Instantiate(prefab, _transform.position, prefab.transform.rotation);

            this.OnDead?.Invoke();
        }

        private void AnimateDamage()
        {
            if (_damageAnimation.IsActive())
                _damageAnimation.Kill();

            _damageAnimation = DOVirtual.Float(
                0f,
                1f,
                _visualConfig.HitDuration,
                progress => _material?.SetFloat(_visualConfig.HitPropertyName,
                    _visualConfig.HitAnimationCurve.Evaluate(progress))
            ).SetLink(_renderer.gameObject);

            if (_visualConfig.DamageSfx)
                _audioSource.PlayOneShot(_visualConfig.DamageSfx);
        }
    }
}