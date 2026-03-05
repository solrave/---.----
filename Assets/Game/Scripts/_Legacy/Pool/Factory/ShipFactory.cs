// using System;
// using UnityEngine;
// using UnityEngine.Audio;
//
// namespace Game
// {
//     public class ShipFactory : IShipFactory
//     {
//         private readonly IDataProvider _dataProvider;
//         private Transform _container;
//         
//         public ShipFactory(IDataProvider dataProvider, Transform container)
//         {
//             _dataProvider = dataProvider;
//             _container = container;
//         }
//
//         public Ship Get(TeamType type)
//         {
//             GameObject prefab;
//             switch (type)
//             {
//                 case TeamType.Player:
//                     prefab = _dataProvider.GetPlayerShipPrefab();
//                     break;
//                 
//                 case TeamType.Enemy:
//                     prefab =  _dataProvider.GetEnemyShipPrefab();
//                     break;
//                 
//                 default: throw new InvalidOperationException($"Invalid team type: {type}");
//             }
//             var coreConfig = _dataProvider.GetShipCoreConfig(type);
//             var visualConfig = _dataProvider.GetShipVisualConfig(type);
//             var ship = GameObject.Instantiate(prefab, _container);
//             var shipLinks  = ship.GetComponent<ShipLinks>();
//             var moveComponent = new MoveComponent(shipLinks.rb, coreConfig);
//             var animationComponent = new AnimationComponent(visualConfig,shipLinks.meshRenderer, shipLinks.audioSource);
//             ship.SetActive(false);
//             return new Ship(ship, coreConfig, moveComponent,animationComponent);
//         }
//     }
// }