using UnityEngine;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
        private BulletCoreConfig _coreConfig;
        private BulletVisualConfig _visualConfig;

        public void Init(BulletCoreConfig coreConfig, BulletVisualConfig visualConfig)
        {
            _coreConfig = coreConfig;
            _visualConfig = visualConfig;
        }

        private void OnTriggerEnter2D(Collider2D other) => this.OnTriggerEntered?.Invoke(this, other);
    }
}