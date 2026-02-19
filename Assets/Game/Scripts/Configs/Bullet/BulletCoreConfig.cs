using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    // R
    [CreateAssetMenu(
        fileName = "BulletCoreConfig",
        menuName = "Game/New BulletCoreConfig"
    )]
    public sealed class BulletCoreConfig : ScriptableObject
    {

        [field: SerializeField]
        public TeamType Team { get; private set; } = TeamType.None;
        
        [field: SerializeField]
        public int Damage { get; private set; }
        
        [field: SerializeField]
        public float Speed { get; private set; }
        
    }
}