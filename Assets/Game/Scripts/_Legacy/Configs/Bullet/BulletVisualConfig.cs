using UnityEngine;

namespace Game
{
    // R
    [CreateAssetMenu(
        fileName = "BulletVisualConfig",
        menuName = "Game/New BulletVisualConfig"
    )]
    public sealed class BulletVisualConfig : ScriptableObject
    {
        [field: SerializeField]
        public TeamType Team { get; private set; } = TeamType.None;
        
        [field: SerializeField]
        public ParticleSystem BulletView { get; private set; }
        
        [field: SerializeField]
        public ParticleSystem ImpactView { get; private set; }
    }
}