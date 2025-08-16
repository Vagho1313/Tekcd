namespace CardGame
{
    public class GamePlayManager : BaseGameController
    {
        public void Start()
        {
            Container.LevelController.OnLevelPassed += (LevelData levelData) =>
            {
                Container.GameDataManager.SaveLevel(levelData);
            };

            Container.GameDataManager.GetLevelData((LevelData levelData) =>
            {
                Container.LevelController.CreateLevel(levelData);
            });
        }
    }
}
