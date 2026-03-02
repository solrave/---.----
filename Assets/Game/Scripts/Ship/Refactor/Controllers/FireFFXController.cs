using UnityEngine;

namespace Game
{
    public class FireVFXController : MonoBehaviour
    {
        [SerializeField] private ShipFireComponent _shipFireComponent;
        [SerializeField] private ParticleSystem _particleSystem;

        private void OnEnable() => _shipFireComponent.OnFire += HandleFire;

        private void OnDisable() => _shipFireComponent.OnFire -= HandleFire;

        private void HandleFire() => _particleSystem.Play();
    }
}