// using Modules.UI;
// using Modules.Utils;
// using UnityEngine;
//
// namespace Game
// {
//     // R
//     public sealed class Player : Ship
//     {
//        
//
//         // [Header("UI")]
//         // [SerializeField]
//         // private GameOverView _gameOverView; // ЭТОГО ТУТ БЫТЬ НЕ ДОЛЖНО
//         //
//         // [SerializeField]
//         // private HealthView _healthView; // ЭТОГО НАВЕРНОЕ ТОЖЕ
//
//
//         public Player(GameObject ship, ShipCoreConfig config, IMoveComponent moveComponent,
//             IAnimationComponent animationComponent) : base(ship, config, moveComponent, animationComponent) { }
//
//         private void OnEnable()
//         {
//             _inputReader = new InputReader();
//             this.OnHealthChanged += UpdateUIAndShake;
//             this.OnDead += _gameOverView.Show;
//         }
//         
//         private void OnDisable()
//         {
//             this.OnHealthChanged -= UpdateUIAndShake;
//             this.OnDead -= _gameOverView.Show;
//         }
//
//         public void Update()
//         {
//             if (_inputReader.ShootIsTriggered) 
//                 this.Fire();
//             
//             if (this._currentHealth > 0) 
//             {
//                 _moveComponent.SetDirection(_inputReader.MoveDirection);
//             }
//         }
//
//         protected override void LateUpdate()
//         {
//             base.LateUpdate();
//             this.transform.position = _playerArea.ClampInBounds(this.transform.position);
//         }
//         
//         private void UpdateUIAndShake(int health)
//         {
//             _healthView.SetHealth(health, this._coreConfig.Health);
//             _cameraShaker.Shake();
//         }
//     }
// }