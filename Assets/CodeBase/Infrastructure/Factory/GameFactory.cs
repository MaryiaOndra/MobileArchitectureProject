using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets assetses;

        public GameFactory(IAssets assetses)
        {
            this.assetses = assetses;
        }
        public GameObject CreateHero(GameObject at) =>
            assetses.Instantiate(AssetPath.HeroPrefabPath, at.transform.position);

        public void CreateHud() =>
            assetses.Instantiate(AssetPath.HudPath);
    }
}