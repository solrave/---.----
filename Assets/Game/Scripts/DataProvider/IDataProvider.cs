using Game;
using Game.Scripts;

public interface IDataProvider
{
    BulletCoreConfig GetBulletCoreConfig(TeamType teamType);
    BulletVisualConfig GetBulletVisualConfig(TeamType teamType);
    ShipCoreConfig GetShipCoreConfig(TeamType teamType);
    ShipVisualConfig GetShipVisualConfig(TeamType teamType);
}