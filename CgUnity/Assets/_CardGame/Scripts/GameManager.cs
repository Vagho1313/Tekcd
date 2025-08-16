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

            mContainer = new ManagersContainer(controllersContainer).
            Setup(new GamePlayManager()).
            Setup(gameUIController).
            Setup(new LevelController()).
            Setup(new CardsController()).
            Setup(new GameDataManager());
        }
    }
}
