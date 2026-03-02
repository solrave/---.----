using System;
using UnityEngine;

namespace Game
{
    public class FireController : MonoBehaviour
    {
        [SerializeField] private ShipFireComponent _shipFireComponent;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
                _shipFireComponent.Fire();
        }
    }
}