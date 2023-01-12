using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets assets;
        private static readonly float _instantiatePointOffset = 0.5f;

        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public List<ISavedProgress> ProgressWriters { get; } = new();

        public GameObject HeroGameObject { get; private set; }
        public event Action HeroCreated;

        public GameFactory(IAssets assets)
        {
            this.assets = assets;
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.HeroPrefabPath, InstantiateHeroPosition(at));
            HeroCreated?.Invoke();
            return HeroGameObject;
        }

        private static Vector3 InstantiateHeroPosition(GameObject at) =>
            new Vector3(at.transform.position.x, at.transform.position.y+ _instantiatePointOffset, at.transform.position.z);

        public GameObject CreateHud() =>
            InstantiateRegistered(AssetPath.HudPath);

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            ProgressReaders.Add(progressReader); 
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
    }
}