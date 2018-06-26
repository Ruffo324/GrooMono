using DinoJumpKi.Exceptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DinoJumpKi
{
    internal class SpriteClass
    {
        private const float Hitboxscale = .5f;

        private static Game _game;
        public readonly Vector2 ExactSize;

        public readonly Texture2D Texture;
        public float Angle;
        public float Da;
        public float Dx;
        public float Dy;
        public float Scale;
        public float X;
        public float Y;

        public SpriteClass(string contentName, float scale)
        {
            if (_game == null)
                throw new SpriteException(
                    "You have to call \"SpriteClass.SetGame(game)\" first, before use the SpriteClass.");

            Scale = scale;

            if (Texture != null)
                return;

            Texture = _game.Content.Load<Texture2D>(contentName);
            ExactSize = new Vector2(Texture.Width * scale, Texture.Height * scale);
        }

        public static void SetGame(Game game)
        {
            _game = game;
        }

        public void Update(float elapsedTime)
        {
            X += Dx * elapsedTime;
            Y += Dy * elapsedTime;
            Angle += Da * elapsedTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 spritePosition = new Vector2(X, Y);
        }

        public bool RectangleCollision(SpriteClass otherSprite)
        {
            if (X + Texture.Width * Scale * Hitboxscale <
                otherSprite.X - otherSprite.Texture.Width * otherSprite.Scale)
                return false;
            if (Y + Texture.Height * Scale * Hitboxscale <
                otherSprite.Y - otherSprite.Texture.Height * otherSprite.Scale)
                return false;
            if (X - Texture.Width * Scale * Hitboxscale >
                otherSprite.X + otherSprite.Texture.Width * otherSprite.Scale)
                return false;
            if (Y - Texture.Height * Scale * Hitboxscale >
                otherSprite.Y + otherSprite.Texture.Height * otherSprite.Scale)
                return false;
            return true;
        }
    }
}