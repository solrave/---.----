using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class FireVFXController : MonoBehaviour
    {
        [SerializeField] private FireComponent _fireComponent;
        [SerializeField] private ParticleSystem _particleSystem;

        private void OnEnable() => _fireComponent.OnFire += HandleFire;

        private void OnDisable() => _fireComponent.OnFire -= HandleFire;

        private void HandleFire() => _particleSystem.Play();
    }
}