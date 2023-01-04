using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assets;

        public GameFactory(IAssetProvider assets)
        {
            this.assets = assets;
        }
        public GameObject CreateHero(GameObject at) =>
            assets.Instantiate(AssetPath.HeroPrefabPath, at.transform.position);

        public void CreateHud() =>
            assets.Instantiate(AssetPath.HudPath);
    }
}