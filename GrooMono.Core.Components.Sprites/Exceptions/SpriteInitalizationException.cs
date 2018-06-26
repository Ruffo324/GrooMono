using System;

namespace GrooMono.Core.Components.Sprites.Exceptions
{
    public class SpriteInitalizationException : Exception
    {
        public SpriteInitalizationException(string message) : base(message)
        {
        }
    }
}