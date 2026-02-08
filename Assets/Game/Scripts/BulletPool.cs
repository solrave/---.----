using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{//R
    public class BulletPool
    {
        private BulletFactory _enemyBulletFactory;
        private BulletFactory _playerBulletFactory;
        private readonly Stack<Bullet> _pool = new();
        private readonly List<Bullet> _bullets = new();

        public BulletPool(BulletConfig config)
        {
            _enemyBulletFactory = new EnemyBulletFactory(config);
            _playerBulletFactory = new PlayerBulletFactory(config);
        }
    }

    public abstract class BulletFactory
    {
        protected Bullet _prefab;
        protected BulletConfig _viewConfig;
        public abstract Bullet GetBullet();
        
        protected BulletFactory(BulletConfig viewConfig)
        {
            _viewConfig = viewConfig;
        }
    }

    public class EnemyBulletFactory : BulletFactory
    {
        public EnemyBulletFactory(BulletConfig viewConfig) : base(viewConfig) { }

        public override Bullet GetBullet()
        {
            return null;
        }
    }
    
    public class PlayerBulletFactory : BulletFactory
    {
        public PlayerBulletFactory(BulletConfig viewConfig) : base(viewConfig) { }

        public override Bullet GetBullet()
        {
            return null;
        }
    }

    public class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collider2D> OnTriggerEntered;

        public TeamType Team { get; private set; } = TeamType.None;
        public Vector2 Direction { get; private set; }
        

        public ParticleSystem BulletView { get; }

        private void OnTriggerEnter2D(Collider2D other) => this.OnTriggerEntered?.Invoke(this, other);
    }
    
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Game/New BulletViewConfig"
    )]
    public sealed class BulletConfig : ScriptableObject
    {
        [field: SerializeField]
        public ParticleSystem BulletView { get; private set; }
        
        [field: SerializeField]
        public int Damage { get; private set; }
        
        [field: SerializeField]
        public float Speed { get; private set; }
    }
    
}