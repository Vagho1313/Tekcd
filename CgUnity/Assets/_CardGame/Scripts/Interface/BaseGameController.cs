namespace CardGame
{
    public abstract class BaseGameController
    {
        protected ManagersContainer Container { get; private set; }

        public void Setup(ManagersContainer container)
        {
            Container = container;
        }
    }
}
