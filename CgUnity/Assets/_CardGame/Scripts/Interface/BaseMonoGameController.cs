using UnityEngine;

namespace CardGame
{
    public abstract class BaseMonoGameController : MonoBehaviour
    {
        protected ManagersContainer Container { get; private set; }

        public void Setup(ManagersContainer container)
        {
            Container = container;
        }
    }
}