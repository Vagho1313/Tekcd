namespace CardGame
{
    public class ManagersContainer
    {
        public ControllersContainer Controllers { get; private set; }

        public CardGameConfig GameConfig { get; private set; }

        public GamePlayManager GamePlayManager { get; private set; }

        public GameUIController GameUIController { get; private set; }

        public LevelController LevelController { get; private set; }

        public CardsController CardsController { get; private set; }

        public GameDataManager GameDataManager { get; private set; }

        public AudioContainer AudioContainer { get; private set; }

        public ManagersContainer(ControllersContainer controllers, CardGameConfig cardGameConfig)
        {
            Controllers = controllers;
            GameConfig = cardGameConfig;
        }

        public ManagersContainer Setup(GamePlayManager gamePlayManager)
        {
            gamePlayManager.Setup(this);
            GamePlayManager = gamePlayManager;
            return this;
        }

        public ManagersContainer Setup(GameUIController gameUIController)
        {
            gameUIController.Setup();
            GameUIController = gameUIController;
            return this;
        }

        public ManagersContainer Setup(LevelController levelController)
        {
            levelController.Setup(this);
            LevelController = levelController;
            return this;
        }

        public ManagersContainer Setup(CardsController cardsController)
        {
            cardsController.Setup(this);
            CardsController = cardsController;
            return this;
        }

        public ManagersContainer Setup(GameDataManager gameDataManager)
        {
            gameDataManager.Setup(this);
            GameDataManager = gameDataManager;
            return this;
        }

        public ManagersContainer Setup(AudioContainer audioContainer)
        {
            audioContainer.Setup(this);
            AudioContainer = audioContainer;
            return this;
        }
    }
}
