using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Services.PersistentProgress;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState

    {
        private const string InitialLevelName = "GameScene";
        private readonly GameStateMachine gameStateMachine;
        private readonly IPersistentProgressService progressService;
        private readonly ISaveLoadService saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, 
            IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            this.gameStateMachine = gameStateMachine;
            this.progressService = progressService;
            this.saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOnInitNew();
            gameStateMachine.Enter<LoadLevelState, string>(progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressOnInitNew()
        {
            progressService.Progress = saveLoadService.LoadProgres() ?? NewProgress();
        }

        private PlayerProgress NewProgress() => 
            new PlayerProgress(initialLevel: InitialLevelName);
    }
}