using Microsoft.Xna.Framework;

namespace GrooMono.Core.GrooGame
{
    public delegate void ManagerUpdateEventHandler(GameTime gameTime);

    public class ManagerUpdate : Manager
    {
        public event ManagerUpdateEventHandler OnUpdate;

        internal void ManageNow(GameTime gameTime)
        {
            OnOnInitialization(gameTime);
            base.ManageNow();
        }

        private void OnOnInitialization(GameTime gametime)
        {
            OnUpdate?.Invoke(gametime);
        }
    }
}