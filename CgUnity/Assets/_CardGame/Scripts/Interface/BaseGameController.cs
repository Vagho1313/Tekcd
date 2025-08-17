namespace CardGame
{
    public abstract class BaseGameController
    {
        protected ManagersContainer Container { get; private set; }

        public virtual void Setup(ManagersContainer container)
        {
            Container = container;
        }
    }
}
