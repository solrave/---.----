using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class PlayerSettings
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
    }
}