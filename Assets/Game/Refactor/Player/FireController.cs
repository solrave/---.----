using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class FireController : MonoBehaviour
    {
        [SerializeField] private FireComponent _fireComponent;
        [SerializeField] private CooldownComponent _cooldownComponent;

        private void Update()
        {
            if (!_cooldownComponent.IsExpired) return;

            if (Input.GetKey(KeyCode.Space))
            {
                _fireComponent.Fire();
                _cooldownComponent.Reset();
            }
        }
    }
}