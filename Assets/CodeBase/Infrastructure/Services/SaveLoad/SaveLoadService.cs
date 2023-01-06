using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        
        private readonly IPersistentProgressService progressService;
        private readonly IGameFactory gameFactory;

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            this.progressService = progressService;
            this.gameFactory = gameFactory;
        }
        
        public void SaveProgress()
        {
            foreach (var progressWriter in gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(progressService.Progress);
            
            PlayerPrefs.SetString(ProgressKey, progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgres() =>
            PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
    }
}