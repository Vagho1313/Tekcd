using MyUtilities;
using UnityEngine;

namespace CardGame
{
    public class ControllersContainer : MonoBehaviour
    {
        [SerializeField] private BaseCardController cardControllerPrefab;
        [SerializeField] private Transform tableSpace;
        [SerializeField] private Transform cardsContainer;
        [SerializeField] private Camera mainCamera;

        public Vector2 TableSpaceSize => new Vector2(tableSpace.lossyScale.x, tableSpace.lossyScale.z);

        public Vector2 TableAnchor => new Vector2(tableSpace.position.x, tableSpace.position.z) - 0.5f * TableSpaceSize;

        public Vector2 GetRayCastPoint()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            Vector3 castPoint = ray.CastPlane(tableSpace);

            return new Vector2(castPoint.x, castPoint.z);
        }

        public BaseCardController CreateCardController()
        {
            return Instantiate(cardControllerPrefab, cardsContainer);
        }
    }
}
