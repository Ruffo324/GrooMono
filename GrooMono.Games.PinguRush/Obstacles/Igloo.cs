using GrooMono.Core.Components;

namespace GrooMono.Games.PinguRush.Obstacles
{
    //TODO: Add possibility to destroy igloo's.
    public class Igloo : Obstacle
    {
        public Igloo(PinguGame pinguGame) : base(pinguGame,
            new Entity("sprites/Igloo", 0.06f) {OutOfWindowAllowed = true})
        {
        }
    }
}