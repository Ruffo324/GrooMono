using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GrooMono.Core.GrooGame
{
    public delegate void ManagerUpdateEventHandler(GameTime gameTime, KeyboardState keyboardState);

    public class ManagerUpdate : Manager
    {
        public event ManagerUpdateEventHandler OnUpdate;

        internal void ManageNow(GameTime gameTime, KeyboardState keyboardState)
        {
            OnOnUpdate(gameTime, keyboardState);
            base.ManageNow();
        }

        private void OnOnUpdate(GameTime gametime, KeyboardState keyboardState)
        {
            OnUpdate?.Invoke(gametime, keyboardState);
        }
    }
}