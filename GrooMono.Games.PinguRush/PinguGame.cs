using System;
using GrooMono.Core.Components;
using GrooMono.Core.Components.Sprites.Models;
using GrooMono.Core.GrooGame;
using GrooMono.Games.PinguRush.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GrooMono.Games.PinguRush
{
    /// <summary>
    ///     Pingu game main entry point.
    /// </summary>
    public class PinguGame : GameInstance
    {
        internal Handle GroundHandle;
        private Pinguin _pinguin;

        public PinguGame()
        {
            ManagerInitialization.OnInitialization += OnInitialization;
            ManagerUpdate.OnUpdate += OnUpdate;
        }

        private void OnUpdate(GameTime gametime, KeyboardState keyboardState)
        {
            // Quit the game if Escape is pressed.
            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();
        }

        private void OnInitialization()
        {
            GroundHandle =
                new Handle("sprites/ice", 1f) {Position = new Geometric2DState(0, ScreenSize.Height * Skyratio)};

            _pinguin = new Pinguin(this);
        }
    }
}