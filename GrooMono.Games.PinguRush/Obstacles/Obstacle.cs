using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrooMono.Core.Components;
using GrooMono.Games.PinguRush.Exceptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GrooMono.Games.PinguRush.Obstacles
{
    public abstract class Obstacle
    {
        private readonly PinguGame _pinguGame;
        internal Entity Entity;

        protected Obstacle(PinguGame pinguGame, Entity entity)
        {
            _pinguGame = pinguGame;
            Entity = entity;
            
            // Set start position, right out of window size
            Entity.Position.X = _pinguGame.ScreenSize.Width + Entity.Size.Width;
            Entity.Position.Y = _pinguGame.GroundHandle.Position.Y - Entity.Size.Height;

            // Listen to events.
            _pinguGame.ManagerUpdate.OnUpdate += OnUpdate;

        }

        private void OnUpdate(GameTime gametime, KeyboardState keyboardstate)
        {
            Entity.Move.Left();
            if (Entity.Position.X < _pinguGame.ScreenSize.Width - Entity.Size.Width)
                _pinguGame.ObstacleDone(this);
        }
    }
}
