using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class FireVFXController : MonoBehaviour
    {
        [FormerlySerializedAs("_shipFireComponent")] [SerializeField] private FireComponent fireComponent;
        [SerializeField] private ParticleSystem _particleSystem;

        private void OnEnable() => fireComponent.OnFire += HandleFire;

        private void OnDisable() => fireComponent.OnFire -= HandleFire;

        private void HandleFire() => _particleSystem.Play();
    }
}