using System;

namespace GrooMono.Core.GrooGame.Exceptions
{
    internal class GameInstanceException : Exception
    {
        public GameInstanceException(string message) : base(message)
        {
        }
    }
}