using System;
using CodeBase.Data;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private MonsterTypeId monsterType;
        [SerializeField] private bool _slain;
        
        private string _id;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.killData.clearedSpawners.Contains(_id))
                _slain = true;
            else
                Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if(_slain)
                progress.killData.clearedSpawners.Add(_id);
        }

        private void Spawn()
        {
            
        }
    }
}