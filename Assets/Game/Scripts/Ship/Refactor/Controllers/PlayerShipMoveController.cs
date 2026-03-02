using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class PlayerShipMoveController : MonoBehaviour
    {
        [FormerlySerializedAs("_shipMoveComponent")] [SerializeField] private MoveComponent moveComponent;

        private void Update()
        {
            var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveComponent.SetDirection(direction);
        }
    }
}