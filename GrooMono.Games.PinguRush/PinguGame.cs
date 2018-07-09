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
        public readonly Random Random = new Random();
        private Pinguin _pinguin;
        internal Handle GroundHandle;
        internal List<Obstacle> Obstacles = new List<Obstacle>();

        public PinguGame()
        {
            ManagerInitialization.OnInitialization += OnInitialization;
            ManagerUpdate.OnUpdate += OnUpdate;
        }

        private void OnUpdate(GameTime gametime, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();
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
            Obstacles.Add(new Igloo(this));
        }
    }
}