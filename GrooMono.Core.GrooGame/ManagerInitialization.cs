namespace GrooMono.Core.GrooGame
{
    public delegate void ManagerInitializationEventHandler();

    public class ManagerInitialization : Manager
    {
        public event ManagerInitializationEventHandler OnInitialization;

        protected internal override void ManageNow()
        {
            OnOnInitialization();
            base.ManageNow();
        }

        private void OnOnInitialization()
        {
            OnInitialization?.Invoke();
        }
    }
}