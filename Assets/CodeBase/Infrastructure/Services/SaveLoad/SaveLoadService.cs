using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string Progress = "Progress";

        public void SaveProgress()
        {
        }

        public PlayerProgress LoadProgres() =>
            PlayerPrefs.GetString(Progress)?
                .ToDeserialized<PlayerProgress>();
    }
}