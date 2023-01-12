using CodeBase.CameraLogic;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialTag";
        private const string EnemySpawner = "EnemySpawner";
        
        private readonly GameStateMachine stateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly LoadingCurtain loadingCurtain;
        private readonly IGameFactory gameFactory;
        private readonly IPersistentProgressService progressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            this.stateMachine = stateMachine;
            this.sceneLoader = sceneLoader;
            this.loadingCurtain = loadingCurtain;
            this.gameFactory = gameFactory;
            this.progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            loadingCurtain.Show();
            gameFactory.CleanUp();
            sceneLoader.Load(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            
            stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in gameFactory.ProgressReaders)
                progressReader.LoadProgress(progressService.Progress);
        }

        private void InitGameWorld()
        {
            InitSpawners();
            GameObject hero = InitHero();
            InitHud(hero);
            CameraFollow(hero);
        }

        private void InitSpawners()
        {
            foreach (GameObject spawner in GameObject.FindGameObjectsWithTag(EnemySpawner))
            {
                EnemySpawner enemySpawner = spawner.GetComponent<EnemySpawner>();
                gameFactory.Register(enemySpawner);
            }
        }

        private void InitHud(GameObject hero)
        {
           GameObject hud = gameFactory.CreateHud();
           HeroHealth heroHealth = hero.GetComponent<HeroHealth>();
           hud.GetComponentInChildren<ActorUI>().Construct(heroHealth);
        }

        private GameObject InitHero() =>
            gameFactory.CreateHero(at: GameObject.FindWithTag(InitialPointTag));

        private void CameraFollow(GameObject hero)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(hero);
        }

        public void Exit() =>
            loadingCurtain.Hide();
    }
}