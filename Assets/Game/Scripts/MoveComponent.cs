// using System;
// using UnityEngine;
//
// namespace Game
// {
//     // +
//     
//     public sealed class MoveComponent : IMoveComponent
//     {
//         public event Action<Vector3> OnMoved;
//         public Vector2? Direction => _direction;
//         
//         private Rigidbody2D _rigidbody;
//         
//         private float _speed;
//         private float _rotationAngle;
//         private Vector2? _direction;
//
//         public void SetSpeed(float speed) => _speed = speed;
//
//         public void SetDirection(Vector2? direction) => _direction = direction;
//
//         public MoveComponent(Rigidbody2D rigidbody, IMoveData moveData)
//         {
//             _rigidbody = rigidbody;
//             _speed = moveData.MoveSpeed;
//             _rotationAngle = moveData.MoveRotationAngle;
//         }
//
//         
//         
//         
//     }
//
//     public interface IMoveComponent
//     {
//         Vector2? Direction { get; }
//         void SetSpeed(float speed);
//         void SetDirection(Vector2? direction);
//         void Move();
//         void ApplyTilt(float deltaTime);
//     }
// }