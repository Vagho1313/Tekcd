using UnityEngine;

namespace CardGame
{
    public class ControllersContainer : MonoBehaviour
    {
        [SerializeField] private BaseCardController cardControllerPrefab;
        [SerializeField] private Transform tableSpace;
        [SerializeField] private Transform cardsContainer;

        public Vector2 TableSpaceSize => new Vector2(tableSpace.lossyScale.x, tableSpace.lossyScale.z);

        public BaseCardController CreateCardController()
        {
            return Instantiate(cardControllerPrefab, cardsContainer);
        }
    }
}
