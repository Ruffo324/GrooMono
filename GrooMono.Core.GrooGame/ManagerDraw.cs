using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrooMono.Core.GrooGame
{
    public delegate void ManagerDrawEventHandler(GameTime gameTime, SpriteBatch spriteBatch);

    public class ManagerDraw : Manager
    {
        public event ManagerDrawEventHandler OnDraw;

        internal void ManageNow(GameTime gameTime, SpriteBatch spriteBatch)
        {
            OnOnInitialization(gameTime, spriteBatch);
            base.ManageNow();
        }

        private void OnOnInitialization(GameTime gameTime, SpriteBatch spriteBatch)
        {
            OnDraw?.Invoke(gameTime, spriteBatch);
        }
    }
}