using UnityEngine;

namespace Game
{
    // +
    [CreateAssetMenu(
        fileName = "BulletViewConfig",
        menuName = "Game/New BulletViewConfig"
    )]
    public sealed class BulletViewConfig : ScriptableObject
    {
        [field: SerializeField]
        public ParticleSystem BlueVFX { get; private set; }
        
        [field: SerializeField]
        public ParticleSystem RedVFX { get; private set; }
        
        [field: SerializeField]
        public ParticleSystem ExplosionVFX  { get; private set; }
    }
}