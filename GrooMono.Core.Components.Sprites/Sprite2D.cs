using System.Drawing;
using System.Net.Mime;
using System.Windows.Forms;
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
        private static float _hitboxScaleDefault = ScaleToWindowSize(0.1f);
        private readonly float _hitboxscale = _hitboxScaleDefault;
        public readonly string ContentName;
        public readonly float Scale;
        public Geometric2DState Movement = new Geometric2DState(0, 0);
        public Geometric2DState Position = new Geometric2DState(0, 0);
        public SizeF Size;

        public Texture2D Texture;

        public Sprite2D(string contentName, float scale)
        {
            // Load content & set values.
            ContentName = contentName;
            Scale = scale;
            ChangeContent(contentName);
        }

        public void ChangeContent(string contentName)
        {
            SizeF oldSize = Size;

            // Load new texture, set size.
            Texture = GameInstance.Instance.Content.Load<Texture2D>(contentName);
            Size = new SizeF(Texture.Width * Scale, Texture.Height * Scale);

            // Adjust position to new texture size.
            Position.Y = Position.Y + (oldSize.Height - Size.Height);
            Position.X = Position.X + (oldSize.Width - Size.Width);
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
            if (Position.X <= sprite2D.Position.X + sprite2D.Size.Width && Position.X >= sprite2D.Position.X)
                if ((Position.Y + Size.Height >= sprite2D.Position.Y + sprite2D.Size.Height &&
                     Position.Y <= sprite2D.Position.Y + sprite2D.Size.Height))
                {
                    Application.DoEvents();
                    return true;
                }

            return false;
        }

        /// <summary>
        ///     Scales the given float to the game window size.
        ///     //TODO: Make this function working correctly.
        /// </summary>
        /// <param name="f">float value wich should be scaled.</param>
        /// <returns>Scaled float value.</returns>
        public static float ScaleToWindowSize(float f)
        {
            return GameInstance.Instance.ScreenSize.Width / GameInstance.Instance.ScreenSize.Height * f;
        }
    }
}