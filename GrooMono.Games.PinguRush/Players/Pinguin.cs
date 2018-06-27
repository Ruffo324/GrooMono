using System;
using GrooMono.Core.Components;
using GrooMono.Core.GrooGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GrooMono.Games.PinguRush.Players
{
    public class Pinguin
    {
        private readonly GameInstance _pinguGame;
        internal Entity PinguinEntity;

        public Pinguin(GameInstance pinguGame)
        {
            _pinguGame = pinguGame;

            // Add main (pinguin) and some content rules.
            PinguinEntity = new Entity("sprites/pinguin", 0.1f);

            PinguinEntity.AddContentRule("sprites/pinguin_jump", entity =>
                entity.Movement.Y < -0 ||
                entity.Movement.Y > 0 && !pinguGame.LastKeyboardState.IsKeyDown(Keys.Down));

            PinguinEntity.AddContentRule("sprites/pinguin_slide", entity =>
                entity.Position.Y + entity.Size.Height >=
                pinguGame.ScreenSize.Height * pinguGame.Skyratio - 0.1
                && pinguGame.LastKeyboardState.IsKeyDown(Keys.Down));

            // Listen to events.
            pinguGame.ManagerUpdate.OnUpdate += OnUpdate;
        }

        private void OnUpdate(GameTime gameTime, KeyboardState keyboardState)
        {
            // Player jump
            if ((keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.Up)) &&
                Math.Abs(PinguinEntity.Movement.Y) < 0.1)
                PinguinEntity.Move.Up();
            else if (keyboardState.IsKeyDown(Keys.Down))
                PinguinEntity.Move.Down();

            // Handle left and right
            if (keyboardState.IsKeyDown(Keys.Left))
                PinguinEntity.Move.Left();
            else if (keyboardState.IsKeyDown(Keys.Right))
                PinguinEntity.Move.Right();
            else
                PinguinEntity.Move.StopX();
        }
    }
}