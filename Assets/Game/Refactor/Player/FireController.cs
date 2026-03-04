using UnityEngine;

namespace Game
{
    public class FireController : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space)) 
                _player.Fire();
        }
    }
}