using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class FireController : MonoBehaviour
    {
        [SerializeField] private FireComponent fireComponent;
        [FormerlySerializedAs("_cooldown")] [SerializeField] private CooldownComponent cooldownComponent;

        private void Update()
        {
            if (!cooldownComponent.IsExpired()) return;

            if (Input.GetKey(KeyCode.Space))
            {
                fireComponent.Fire();
                cooldownComponent.Reset();
            }
        }
    }
}