// using UnityEngine;
//
// namespace Game
// {
//     public class ShipController
//     {
//         private readonly Ship _playerShip;
//         private readonly InputReader _inputReader;
//
//         public ShipController(InputReader inputReader)
//         {
//             _inputReader = inputReader;
//         }
//
//         public void FixedUpdate()
//         {
//             if (_inputReader.ShootIsTriggered) 
//                 _playerShip.Fire();
//             
//             if (_playerShip.CurrentHealth > 0)
//             {
//                 _playerShip.SetDirection(_inputReader.MoveDirection);
//             }
//
//             _playerShip.Move();
//         }
//
//         public void LateUpdate()
//         {
//             _playerShip.ApplyTilt(Time.deltaTime);
//         }
//     }
// }