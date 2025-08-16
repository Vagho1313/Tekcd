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

        private ManagersContainer mContainer;

        private void Awake()
        {
            localDataBaseContainer.Setup();

            mContainer = new ManagersContainer(controllersContainer);

            mContainer.Setup(new GamePlayManager());
            mContainer.Setup(gameUIController);
            mContainer.Setup(new LevelController());
            mContainer.Setup(new CardsController());
            mContainer.Setup(new GameDataManager());
        }
    }
}
