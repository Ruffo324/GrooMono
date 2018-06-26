namespace GrooMono.Core.GrooGame
{
    public delegate void ManagerContentEventHandler();

    public class ManagerContent : Manager
    {
        public event ManagerContentEventHandler OnContentLoad;

        protected internal override void ManageNow()
        {
            OnOnInitialization();
            base.ManageNow();
        }

        private void OnOnInitialization()
        {
            OnContentLoad?.Invoke();
        }
    }
}