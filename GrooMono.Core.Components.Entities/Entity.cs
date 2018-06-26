using System;
using System.Collections.Generic;
using System.Linq;
using GrooMono.Core.Components.Entities;
using GrooMono.Core.GrooGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

// ReSharper disable once CheckNamespace
namespace GrooMono.Core.Components
{
    public class Entity : Handle
    {
        public readonly Move Move;
        private float _gravity = ScaleToWindowSize(80f);
        private float _movementSpeed = ScaleToWindowSize(1000F);

        public Entity(string contentName, float scale) : base(contentName, scale)
        {
            Move = new Move(this);
            GameInstance.Instance.ManagerUpdate.OnUpdate += OnUpdate;
        }

        public float GameGround { get; set; } =
            GameInstance.Instance.ScreenSize.Height * GameInstance.Instance.Skyratio;

        public float Gravity
        {
            get => _gravity;
            set => _gravity = ScaleToWindowSize(value);
        }

        public float MovementSpeed
        {
            get => _movementSpeed;
            set => _movementSpeed = ScaleToWindowSize(value);
        }

        private void OnUpdate(GameTime gameTime, KeyboardState keyboardState)
        {
            float elapsedTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

            // Apply movement.
            Position.X += Movement.X * elapsedTime;
            Position.Y += Movement.Y * elapsedTime;
            Position.Rotation += Movement.Rotation * elapsedTime;

            // Gravity
            Movement.Y += Gravity;

            // Up / down
            if (Position.Y >= GameGround - Size.Height)
            {
                Movement.Y = 0;
                Position.Y = GameGround - Size.Height;
            }

            // Left / right
            if (Position.X > GameInstance.Instance.ScreenSize.Width - Size.Width)
            {
                Position.X = GameInstance.Instance.ScreenSize.Width - Size.Width;
                Movement.X = 0;
            }
            else if (Position.X < 0)
            {
                Position.X = 0;
                Movement.X = 0;
            }
        }
    }
}