using System.Drawing;
using GrooMono.Core.Components.Sprites.Models;
using GrooMono.Core.GrooGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

// ReSharper disable once CheckNamespace
namespace GrooMono.Core.Components
{
    public class Sprite2D
    {
        private static float _hitboxScaleDefault = .5f;
        private readonly float _hitboxscale = _hitboxScaleDefault;
        public readonly string ContentName;
        public readonly float Scale;
        public readonly SizeF Size;

        public readonly Texture2D Texture;
        public Geometric2DState Movement;
        public Geometric2DState Position;

        public Sprite2D(string contentName, float scale)
        {
            GameInstance gameInstance = GameInstance.Instance;
            // Load content & set values.
            ContentName = contentName;
            Texture = gameInstance.Content.Load<Texture2D>(ContentName);
            Scale = scale;
            Size = new SizeF(Texture.Width * scale, Texture.Height * scale);

            // Set Position and Movement to zero.
            Position = new Geometric2DState(0, 0);
            Movement = new Geometric2DState(0, 0);
        }

        public static void SetDefaultHitboxScale(float hitboxScale)
        {
            _hitboxScaleDefault = hitboxScale;
        }

        public void Update(float elapsedTime)
        {
            Position.X += Movement.X * elapsedTime;
            Position.Y += Movement.Y * elapsedTime;
            Position.Rotation += Movement.Rotation * elapsedTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Vector2(Position.X, Position.Y),
                null, Color.White, Position.Rotation,
                new Vector2(0, 0),
                new Vector2(Scale, Scale), SpriteEffects.None, 0f);
        }

        public bool CollisionWith(Sprite2D sprite2D)
        {
            return !(Position.X + Texture.Width * Scale * _hitboxscale <
                     sprite2D.Position.X - sprite2D.Texture.Width * sprite2D.Scale) &&
                   !(Position.Y + Texture.Height * Scale * _hitboxscale <
                     sprite2D.Position.Y - sprite2D.Texture.Height * sprite2D.Scale) &&
                   !(Position.X - Texture.Width * Scale * _hitboxscale >
                     sprite2D.Position.X + sprite2D.Texture.Width * sprite2D.Scale) &&
                   !(Position.Y - Texture.Height * Scale * _hitboxscale >
                     sprite2D.Position.Y + sprite2D.Texture.Height * sprite2D.Scale);
        }

        /// <summary>
        /// Scales the given float to the game window size.
        /// //TODO: Make this function working correctly.
        /// </summary>
        /// <param name="f">float value wich should be scaled.</param>
        /// <returns>Scaled float value.</returns>
        public static float ScaleToWindowSize(float f)
        {
            return (float) (GameInstance.Instance.ScreenSize.Width / GameInstance.Instance.ScreenSize.Height) * f;
            //return (float) (GameInstance.Instance.ScreenSize.Width / (GameInstance.Instance.ScreenSize.Height * 0.03));
        }
    }
}