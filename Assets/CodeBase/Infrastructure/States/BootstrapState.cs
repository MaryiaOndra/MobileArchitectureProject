using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly AllServices services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            this.sceneLoader = sceneLoader;
            this.gameStateMachine = gameStateMachine;
            this.services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() => 
            gameStateMachine.Enter<LoadLevelState, string>("GameScene");

        private void RegisterServices()
        {
            services.RegisterSingle<IInputService>(InputService());
            services.RegisterSingle<IAssets>(new AssetProvider());
            services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            services.RegisterSingle<IGameFactory>(new GameFactory(services.Single<IAssets>()));
        }

        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }

        public void Exit()
        {
        }
    }
}