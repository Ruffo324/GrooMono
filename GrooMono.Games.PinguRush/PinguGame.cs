using System;
using System.Collections.Generic;
using GrooMono.Core.Components;
using GrooMono.Core.Components.Sprites.Models;
using GrooMono.Core.GrooGame;
using GrooMono.Games.PinguRush.Obstacles;
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
        internal List<Obstacle> Obstacles = new List<Obstacle>();
        private Pinguin _pinguin;

        private readonly Random _random = new Random();

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

            if((int)gametime.TotalGameTime.TotalSeconds != 0)
                if(_random.Next(1, 3) % (int)gametime.TotalGameTime.TotalSeconds == 0)
                    Obstacles.Add(new Igloo(this));
        }

        private void OnInitialization()
        {
            GroundHandle =
                new Handle("sprites/ice", 1f) {Position = new Geometric2DState(0, ScreenSize.Height * Skyratio)};

            _pinguin = new Pinguin(this);
            Obstacles.Add(new Igloo(this));
        }

        public void ObstacleDone(Obstacle obstacle)
        {
            Obstacles.Remove(obstacle);
        }
    }
}