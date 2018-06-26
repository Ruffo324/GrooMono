// ReSharper disable once CheckNamespace

using GrooMono.Core.GrooGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// ReSharper disable once CheckNamespace
namespace GrooMono.Core.Components
{
    public class Handle : Sprite2D
    {
        public Handle(string contentName, float scale) : base(contentName, scale)
        {
            GameInstance.Instance.ManagerDraw.OnDraw += ManagerDrawOnOnDraw;
        }

        public bool Drawable { get; set; } = true;

        private void ManagerDrawOnOnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Drawable)
                return;

            Draw(spriteBatch);
        }
    }
}