// using UnityEngine;
//
// namespace Game
// {
//     // +
//     public sealed class PlayerBulletInstantiator : MonoBehaviour
//     {
//         [SerializeField]
//         private BulletWorldGO _bulletWorld;
//
//         [SerializeField]
//         private Player _player;
//
//         private void OnEnable()
//         {
//             _player.OnFire += this.OnFire;
//         }
//
//         private void OnDisable()
//         {
//             _player.OnFire -= this.OnFire;
//         }
//
//         private void OnFire(ShipID id)
//         {
//             _bulletWorld.Spawn(
//                 _player.firePoint.position,
//                 _player.firePoint.up,
//                 _player.bulletSpeed,
//                 _player.bulletDamage,
//                 TeamType.Player
//             );
//         }
//     }
// }