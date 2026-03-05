using UnityEngine;

namespace Game
{
    // R
    [CreateAssetMenu(
        fileName = "ShipCoreConfig",
        menuName = "Game/New ShipCoreConfig"
    )]
    public sealed class ShipCoreConfig : ScriptableObject, IHealthData, IMoveData, IFireData
    {
        [field: SerializeField]
        public TeamType Team { get; private set; } = TeamType.None;
        
        [Header("Health")]
        
        [field: SerializeField]
        public int Health { get; private set; } = 5;
        
        [Header("Movement")]
        
        [field: SerializeField]
        public float MoveSpeed { get; private set; } = 5;
        
        [field: SerializeField]
        public float MoveRotationAngle { get; private set; } = 30f;

        [Header("Fire Cooldown")]
        
        [field: SerializeField]
       public float FireCooldown { get; private set; } = 0.25f;
    }
    
    public interface IHealthData
    {
        public int Health { get; }
    }
    
    public interface IMoveData
    {
        public float MoveSpeed { get;}
        public float MoveRotationAngle { get; } 
    }
    
    public interface IFireData
    {
        public float FireCooldown { get; }
    }
    
}