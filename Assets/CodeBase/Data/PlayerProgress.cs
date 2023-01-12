using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State heroState;
        public WorldData worldData;
        public Stats heroStats;
        public KillData killData;

        public PlayerProgress(string initialLevel)
        {
            worldData = new WorldData(initialLevel);
            heroState = new State();
            heroStats = new Stats();
            killData = new KillData();
        }
    }
}