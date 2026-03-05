using Game.Scripts;
using UnityEngine;

namespace Game
{
    public class ShipLinks : MonoBehaviour
    {
        [SerializeField] public Rigidbody2D rb; //Mover
        [SerializeField] public AudioSource audioSource; // Particle
        [SerializeField] public Transform firePoint; //bulletSpawner
        [SerializeField] public MeshRenderer meshRenderer; // Particle
    }
}