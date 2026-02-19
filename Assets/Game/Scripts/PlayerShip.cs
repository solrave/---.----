using Modules.UI;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    // R
    public sealed class PlayerShip : Ship
    {
        [SerializeField]
        private TransformBounds _playerArea;

        [SerializeField]
        private CameraShaker _cameraShaker;

        [Header("UI")]
        [SerializeField]
        private GameOverView _gameOverView;

        [SerializeField]
        private HealthView _healthView;

        private InputReader _inputReader;

        private void OnEnable()
        {
            _inputReader = new InputReader();
            this.OnHealthChanged += UpdateUIAndShake;
            this.OnDead += _gameOverView.Show;
        }
        
        private void OnDisable()
        {
            this.OnHealthChanged -= UpdateUIAndShake;
            this.OnDead -= _gameOverView.Show;
        }

        public void Update()
        {
            if (_inputReader.SpaceKeyPressed) //Добавить события!!
                this.Fire();
            
            if (this._currentHealth > 0) 
            {
                _moveComponent.SetDirection(_inputReader.MoveDirection); //Добавить события!!
            }
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            this.transform.position = _playerArea.ClampInBounds(this.transform.position);
        }
        
        private void UpdateUIAndShake(int health)
        {
            _healthView.SetHealth(health, this._coreConfig.Health);
            _cameraShaker.Shake();
        }
    }
}