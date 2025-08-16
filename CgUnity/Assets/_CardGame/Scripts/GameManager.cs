using MyUtilities;
using UnityEngine;

namespace CardGame
{
    public class GameManager : MonoBehaviour
    {
        [Space(10), Header("Utilities")]
        [SerializeField] private JsonBaseSerializer serializer;
        [SerializeField] private LocalDataBaseContainer localDataBaseContainer;
    }
}
