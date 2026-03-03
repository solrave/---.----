using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private TiltComponent _tiltComponent;

        private void Update()
        {
            var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            _moveComponent.SetDirection(direction);
            _tiltComponent.TiltToDirection(direction);
        }
    }
}