// using System;
// using Modules.Utils;
// using UnityEngine;
// using UnityEngine.Serialization;
//
// namespace Game //R
// {
//     public class GameLoop : MonoBehaviour
//     {   
//         [Header("Data Provider")]
//         [SerializeField] 
//         private DataProvider _dataProvider;
//         
//         [Header("Pool Container")]
//         [SerializeField] 
//         private Transform _poolContainer;
//         
//         [Header("Level Bounds")]
//         [SerializeField]
//         private LevelBounds _playerArea;
//         
//         [Header("Points")]
//         [SerializeField]
//         private Transform _playerStartPosition;
//         
//         [SerializeField]
//         private Transform[] _spawnPositions;
//         
//         [SerializeField]
//         private Transform[] _attackPositions;
//
//         [SerializeField]
//         private CameraShaker _cameraShaker;
//         
//         private PoolManager _pool;
//         private ShipSpawner _shipSpawner;
//         private InputReader _inputReader;
//
//         private void Start()
//         {
//             _pool = new PoolManager(_dataProvider, _poolContainer);
//             _shipSpawner = new ShipSpawner(_pool, _playerStartPosition, _spawnPositions);
//         }
//
//         private void Update()
//         {
//             
//         }
//
//         private void FixedUpdate()
//         {
//            
//         }
//
//         private void LateUpdate()
//         {
//            
//         }
//
//         private void OnEnable()
//         {
//             
//         }
//
//         private void OnDisable()
//         {
//             
//         }
//     }
//
// }