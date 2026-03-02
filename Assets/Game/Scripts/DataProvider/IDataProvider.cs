using Game;
using Game.Configs.Spawner;
using Game.Scripts;
using UnityEngine;

public interface IDataProvider
{
    BulletCoreConfig GetBulletCoreConfig(TeamType teamType);
    BulletVisualConfig GetBulletVisualConfig(TeamType teamType);
    ShipCoreConfig GetShipCoreConfig(TeamType teamType);
    ShipVisualConfig GetShipVisualConfig(TeamType teamType);
    Bullet GetBulletPrefab();
    GameObject GetPlayerShipPrefab();
    GameObject GetEnemyShipPrefab();

    SpawnerConfig GetSpawnerConfig(SpawnerType type);
}

public enum SpawnerType
{
    EnemyShip, EnemyBullet, PlayerShip, PlayerBullet
}