using UnityEngine;

namespace Game
{
    public class ShipFactory : IShipFactory
    {
        private readonly IDataProvider _dataProvider;
        private Transform _container;
        
        public ShipFactory(IDataProvider dataProvider, Transform container)
        {
            _dataProvider = dataProvider;
            _container = container;
        }

        public Ship Get(TeamType type)
        {
            var prefab = _dataProvider.GetShipPrefab();
            var coreConfig = _dataProvider.GetShipCoreConfig(type);
            var visualConfig = _dataProvider.GetShipVisualConfig(type);
            var ship = GameObject.Instantiate(prefab, _container);
            ship.Init(coreConfig,visualConfig);
            ship.gameObject.SetActive(false);
            return ship;
        }
    }
}