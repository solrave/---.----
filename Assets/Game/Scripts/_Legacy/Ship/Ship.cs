// using System;
// using DG.Tweening;
// using Game.Scripts;
// using UnityEngine;
//
// namespace Game
// {
//     // R
//     public class Ship
//     {
//         public event Action<int> OnHealthChanged;
//         public event Action OnDead;
//         public event Action<TeamType> OnFire;
//
//         public int CurrentHealth => _currentHealth;
//         protected int _currentHealth;
//
//         private IMoveComponent _moveComponent;
//         private IAnimationComponent _animationComponent;
//         public readonly GameObject shipGameObject;
//         private ShipLinks _shipLinks;
//         private ShipCoreConfig _coreConfig;
//
//         // private float _fireTime;
//         private AudioSource _audioSource;
//
//         public Ship(GameObject ship, ShipCoreConfig config, IMoveComponent moveComponent,
//             IAnimationComponent animationComponent)
//         {
//             shipGameObject = ship;
//             _currentHealth = config.Health;
//             _moveComponent = moveComponent;
//             _animationComponent = animationComponent;
//         }
//
//         public void SetDirection(Vector2? dir)
//         {
//             _moveComponent.SetDirection(dir);
//         }
//
//         public void SetSpeed(float speed)
//         {
//             _moveComponent.SetSpeed(speed);
//         }
//
//         public void Move()
//         {
//             _moveComponent.Move();
//         }
//
//         public void Fire()
//         {
//             float time = Time.time;
//             if (time - _fireTime < _coreConfig.FireCooldown || this._currentHealth <= 0)
//                 return;
//
//             _animationComponent.PlayFireSound();
//             _animationComponent.PlayFireVisual();
//             //animation controller возможно надо отсюда вынести и подписаться на событие выстрела извне
//
//             this.OnFire?.Invoke(_coreConfig.Team);
//             _fireTime = time;
//         }
//
//         public void ApplyTilt(float deltaTime)
//         {
//             _moveComponent.ApplyTilt(deltaTime);
//         }
//
//         public void NotifyAboutHealthChanged(int health)
//         {
//             if (health > 0)
//                 _animationComponent.AnimateDamage();
//
//             this.OnHealthChanged?.Invoke(health);
//         }
//
//         public void NotifyAboutDead()
//         {
//             _animationComponent.PlayDestroyEffectFor(shipGameObject.transform);
//
//             this.OnDead?.Invoke();
//         }
//     }
//
//     public class ShipRotationComponent : MonoBehaviour
//     {
//         [SerializeField] private Rigidbody2D _rigidbody;
//         [SerializeField] private float _rotationAngle;
//         [SerializeField] private float _rotationSpeed;
//         private Vector2 _direction;
//
//         private void Update()
//         {
//             Rotation(Time.deltaTime);
//         }
//
//         public void Rotation(float deltaTime)
//         {
//             Vector3 shipAngles = _rigidbody.transform.rotation.eulerAngles;
//             shipAngles.x = _rotationAngle * _direction.y;
//             shipAngles.y = _rotationAngle / 2 * _direction.x * -1f;
//
//             Quaternion shipRotation = Quaternion.Euler(shipAngles);
//             float t = _rotationSpeed * deltaTime;
//             _rigidbody.transform.localRotation = Quaternion.Lerp(_rigidbody.transform.localRotation, shipRotation, t);
//         }
//     }
// }