using System.Threading;
using System.Threading.Tasks;
using GrooMono.Core.Components;
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
            if (Entity.Position.X < Entity.Size.Width * -1)
            {
                _pinguGame.ManagerUpdate.OnUpdate -= OnUpdate;

                //TODO: Make this better
                for(int i = 0; i < _pinguGame.Random.Next(1, 2); i++)
                    new Task(() =>
                    {
                        Thread.Sleep(_pinguGame.Random.Next(2, 10) * 100);
                        _pinguGame.ObstacleDone(this);
                    }).Start();
            }
        }
    }
}