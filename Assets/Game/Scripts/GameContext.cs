using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game //R
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField] 
        private DataProvider _dataProvider;
        
        
        [Header("Pool Container")]
        [SerializeField] 
        private Transform _poolContainer;
        
        
        private PoolManager _pool;

        private void Start()
        {
            _pool = new PoolManager(_dataProvider, _poolContainer);
        }

        private void Update()
        {
            
        }

        private void FixedUpdate()
        {
           
        }

        private void LateUpdate()
        {
           
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }
    }

}