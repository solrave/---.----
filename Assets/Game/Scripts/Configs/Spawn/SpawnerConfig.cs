using UnityEngine;

namespace Game.Configs.Spawner
{
    [CreateAssetMenu(
        fileName = "spawnerConfig",
        menuName = "Game/New Spawner config"
    )]
    public class SpawnerConfig : ScriptableObject
    {
        [Header("Spawner Type")]
        
        [field: SerializeField] 
        public SpawnerType Type { get; private set; }
        
        [field: SerializeField] 
        public float MinSpawnCooldown { get; private set; } = 2;
        
        [field: SerializeField] 
        public float MaxSpawnCooldown { get; private set; } = 3;  
    }
}