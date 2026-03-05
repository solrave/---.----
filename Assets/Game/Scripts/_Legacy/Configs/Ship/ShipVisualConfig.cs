using UnityEngine;

namespace Game.Scripts
{ //R
    [CreateAssetMenu(
        fileName = "ShipVisualConfig",
        menuName = "Game/New ShipVisualConfig"
    )]
    public sealed class ShipVisualConfig : ScriptableObject
    {
        [field: SerializeField]
        public TeamType Team { get; private set; } = TeamType.None;
        
        [Header("Visual")]
        [field: SerializeField]
        public Material MaterialPrefab { get; private set; }
        
        [field: SerializeField]
        public AnimationCurve HitAnimationCurve { get; private set; }

        [field: SerializeField]
        public string HitPropertyName { get; private set; } = "_HitBlend";

        [field: SerializeField]
        public float HitDuration { get; private set; } = 0.2f;
        
        [Header("Particle")]
        [field: SerializeField]
        public ParticleSystem DestroyEffectPrefab { get; private set; }
        
        [field: SerializeField]
        public ParticleSystem FireVfx { get; private set; }
        
        [Header("Audio")]
        [field: SerializeField]
        public AudioClip DamageSfx { get; private set; }
        
        [field: SerializeField]
        public AudioClip FireSfx { get; private set; }
        
    }
}