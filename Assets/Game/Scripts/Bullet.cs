using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

namespace Game //R
{ 
    public class Bullet : MonoBehaviour
    {
        public Action<Bullet, Collider2D> OnTriggerEntered;
        
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