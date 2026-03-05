using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private void Update()
        {
            var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
            _player.SetDirection(direction);
        }
    }
}