using UnityEngine;

namespace Game
{
    public class LifeTimeComponent : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;

        private PrefabPool _prefabPool;
        private float _timer;

        private void Start() => _timer = _lifeTime;

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer > 0) return;

            _prefabPool.DeSpawn(transform.gameObject);
        }

        public void SetPrefabPool(PrefabPool prefabPool)
        {
            _prefabPool = prefabPool;
        }

        public void Reset() => _timer = _lifeTime;
    }
}