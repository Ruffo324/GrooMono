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
            Skyratio = 0.9333f; // TODO: Wo anders hin halt
            ManagerInitialization.OnInitialization += OnInitialization;


            //ManagerDraw.OnDraw += OnDraw;
            //ManagerContent.OnContentLoad += OnContentLoad;
            //ManagerInitialization.OnInitialization += OnInitialitzation;
            ManagerUpdate.OnUpdate += OnUpdate;
        }

        private void OnUpdate(GameTime gametime)
        {
            KeyboardState state = Keyboard.GetState();

            // Quit the game if Escape is pressed.
            if (state.IsKeyDown(Keys.Escape))
                Exit();

            // Jump
            //if (state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Up))
            //{
            //    // Jump if the Space is pressed but not held and the dino is on the floor
            //    if (!_spaceDown && _pinguinEntity.Position.Y >= ScreenSize.Height * Skyratio)
            //        _pinguinEntity.Movement.Y -= _pinguinEntity.MovementSpeed;
            //
            //    _spaceDown = true;
            //}
            //else
            //{
            //    _spaceDown = false;
            //}

            if ((state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Up)) && _pinguinEntity.Movement.Y == 0f)
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
            _pinguinEntity = new Entity("sprites/pinguin", 0.1f);
        }
    }
}