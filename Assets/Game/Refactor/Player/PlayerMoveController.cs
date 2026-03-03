using UnityEngine;

namespace Game
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private RotateComponent _rotateComponent;

        private void Update()
        {
            var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _moveComponent.SetDirection(direction);
            _rotateComponent.RotateToDirection(direction);
        }
    }
}