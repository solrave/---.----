using System;
using UnityEngine;

namespace Game //R
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField] 
        private DataProvider _dataProvider;
        
        [Header("Pool Container")]
        [SerializeField] 
        private Transform _container;
        
        
        private PoolManager _pool;

        private void Start()
        {
            _pool = new PoolManager(_dataProvider, _container);
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