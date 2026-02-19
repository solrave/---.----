using System;
using System.Collections.Generic;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    // +
    public sealed class BulletWorldGO : MonoBehaviour
    {
        [SerializeField]
        private BulletCoreConfig _prefab;

        [SerializeField]
        private Transform _container;

        [SerializeField]
        private BulletViewConfig _configView;

        [SerializeField]
        private TransformBounds _levelBounds;

        private readonly Stack<BulletCoreConfig> _pool = new();
        private readonly List<BulletCoreConfig> _bullets = new();

        private void Awake()
        {
            for (var i = 0; i < 10; i++)
            {
                BulletCoreConfig bullet = Instantiate(_prefab, _container);
                bullet.gameObject.SetActive(false);
                _pool.Push(bullet);
            }
        }

        private void FixedUpdate()
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                BulletCoreConfig bullet = _bullets[i];
                Vector3 moveStep = bullet.Direction * bullet.Speed * Time.fixedDeltaTime;
                bullet.transform.position += moveStep;

                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    _bullets.RemoveAt(i);

                    bullet.OnTriggerEntered -= this.OnTriggerEntered;
                    bullet.gameObject.SetActive(false);
                    _pool.Push(bullet);
                }
            }
        }

        public void Spawn(Vector2 position, Vector2 direction, float speed, int damage, TeamType team)
        {
            if (_pool.TryPop(out BulletCoreConfig bullet))
                bullet.gameObject.SetActive(true);
            else
                bullet = Instantiate(_prefab, _container);
            
            bullet.Direction = direction;
            bullet.Speed = speed;
            bullet.Damage = damage;
            bullet.Team = team;

            bullet.transform.position = position;
            bullet.transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
            bullet.gameObject.layer = team switch
            {
                TeamType.None => LayerMask.NameToLayer("Default"),
                TeamType.Player => LayerMask.NameToLayer("PlayerBullet"),
                TeamType.Enemy => LayerMask.NameToLayer("EnemyBullet"),
                _ => throw new ArgumentOutOfRangeException(nameof(team), team, null)
            };

            if (team == TeamType.Player)
            {
                bullet.BlueVFX.SetActive(true);
                bullet.RedVFX.SetActive(false);
            }
            else
            {
                bullet.BlueVFX.SetActive(false);
                bullet.RedVFX.SetActive(true);
            }

            bullet.OnTriggerEntered += this.OnTriggerEntered;
            _bullets.Add(bullet);
        }

        private void OnTriggerEntered(BulletCoreConfig bullet, Collider2D other)
        {
            if (!other.TryGetComponent(out Ship ship)) 
                return;

            if (bullet.Team == TeamType.Player && ship is Enemy ||
                bullet.Team == TeamType.Enemy && ship is PlayerShip)
            {
                // Deal damage to target:
                if (bullet.Damage > 0)
                {
                    ship._currentHealth = Mathf.Clamp(ship._currentHealth - bullet.Damage, 0, ship.config.Health);
                    ship.NotifyAboutHealthChanged(ship._currentHealth);
                    
                    if (ship._currentHealth <= 0)
                    {
                        ship.NotifyAboutDead();
                        ship.gameObject.SetActive(false);
                    }
                }

                bullet.OnTriggerEntered -= this.OnTriggerEntered;

                _bullets.Remove(bullet);

                bullet.gameObject.SetActive(false);
                _pool.Push(bullet);

                // Explosion Vfx
                ParticleSystem prefab = _configView.ExplosionVFX;
                Instantiate(prefab, bullet.transform.position, prefab.transform.rotation);
            }
        }
    }
}