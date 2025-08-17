using System;
using MyUtilities;

namespace CardGame
{
    public class GameDataManager : BaseGameController
    {
        private const string LevelIndex = "LevelIndex";

        public void GetLevelData(Action<LevelData> handler)
        {
            LocalDataBase.GetData(LevelIndex, (bool success, LevelData levelData) =>
            {
                handler?.Invoke(success? levelData : new LevelData { index = 0, result = new GameResult() });
            });
        }

        public void SaveLevel(LevelData levelData)
        {
            LocalDataBase.SaveData(LevelIndex, levelData);
        }

        public void ClearAll()
        {
            LocalDataBase.DeleteData(LevelIndex);
        }
    }
}
