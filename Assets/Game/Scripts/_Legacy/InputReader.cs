using UnityEngine;

namespace Game
{
    public class InputReader : IDirectionSource
    {
        public bool ShootIsTriggered => Input.GetKeyDown(KeyCode.Space);
        public Vector2? MoveDirection => new Vector2(Input.GetAxisRaw("Horizontal"),
                                                    Input.GetAxisRaw("Vertical"));
    
    }

    public class AIMovementSystem : IDirectionSource
    {
        public Vector2? MoveDirection { get; }
    }

    public interface IDirectionSource
    {
        Vector2? MoveDirection { get; }
    }
}