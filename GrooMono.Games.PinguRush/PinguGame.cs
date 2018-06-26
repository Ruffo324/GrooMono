﻿using System;
using GrooMono.Core.Components;
using GrooMono.Core.Components.Sprites.Models;
using GrooMono.Core.GrooGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GrooMono.Games.PinguRush
{
    /// <summary>
    ///     Pingu game main entry point.
    /// </summary>
    public class PinguGame : GameInstance
    {
        private Handle _backgroundHandle;
        private Entity _pinguinEntity;

        public PinguGame()
        {
            ManagerInitialization.OnInitialization += OnInitialization;
            ManagerUpdate.OnUpdate += OnUpdate;
        }

        private void OnUpdate(GameTime gametime)
        {
            KeyboardState state = Keyboard.GetState();

            // Quit the game if Escape is pressed.
            if (state.IsKeyDown(Keys.Escape))
                Exit();

            // Player jump
            if ((state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Up)) && Math.Abs(_pinguinEntity.Movement.Y) < 0.1)
                _pinguinEntity.Move.Up();

            // Handle left and right
            if (state.IsKeyDown(Keys.Left))
                _pinguinEntity.Move.Left();
            else if (state.IsKeyDown(Keys.Right))
                _pinguinEntity.Move.Right();
            else
                _pinguinEntity.Move.StopX();
        }

        private void OnInitialization()
        {
            _backgroundHandle =
                new Handle("sprites/ice", 1f) {Position = new Geometric2DState(0, ScreenSize.Height * Skyratio)};

            // Add main (pinguin) and it's content rules.
            _pinguinEntity = new Entity("sprites/pinguin", 0.1f);
            _pinguinEntity.AddContentRule("sprites/pinguin_jump", (entity) => Math.Abs(entity.Movement.Y) > 0.1);
        }
    }
}