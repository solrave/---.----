using UnityEngine;

namespace Game
{
    public class EnemyFireController : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private EnemyMoveController _moveController;

        private void Update()
        {
            if (_moveController.IsReached)
                _enemy.Fire();
        }
    }
}
