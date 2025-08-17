using MyUtilities;
using UnityEngine;

namespace CardGame
{
    public class GameManager : MonoBehaviour
    {
        [Space(10), Header("Utilities")]
        [SerializeField] private JsonBaseSerializer serializer;
        [SerializeField] private LocalDataBaseContainer localDataBaseContainer;
        [Space(10), Header("Controllers")]
        [SerializeField] private ControllersContainer controllersContainer;
        [SerializeField] private GameUIController gameUIController;
        [Space(10), Header("Config")]
        [SerializeField] private CardGameConfig cardGameConfig;

        private ManagersContainer mContainer;

        private void Awake()
        {
            localDataBaseContainer.Setup();

            mContainer = new ManagersContainer(controllersContainer, cardGameConfig).
            Setup(new GamePlayManager()).
            Setup(gameUIController).
            Setup(new LevelController()).
            Setup(new CardsController()).
            Setup(new GameDataManager());
        }

        private void Start()
        {
            mContainer.GamePlayManager.OnGameEnded += (GameResult result) =>
            {
                mContainer.GamePlayManager.EndGame();
                mContainer.GameUIController.HomeMode();
            };

            mContainer.GameUIController.HomeMode();

            mContainer.GameUIController.OnStartButton += () =>
            {
                mContainer.GameUIController.GameMode();
                mContainer.GamePlayManager.StartGame();
            };

            mContainer.GameUIController.OnEndButton += () =>
            {
                mContainer.GamePlayManager.EndGame();
                mContainer.GameUIController.HomeMode();
            };

            mContainer.GameUIController.OnClearButton += () =>
            {
                mContainer.GameDataManager.ClearAll();
            };
        }

        private void Update()
        {
            if (mContainer.GamePlayManager.Started)
            {
                mContainer.GamePlayManager.Update();
            }
        }
    }
}
