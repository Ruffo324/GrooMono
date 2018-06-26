using System;

namespace GrooMono.Core.GrooGame.Exceptions
{
    internal class ManagerActionException : Exception
    {
        public ManagerActionException(string message) : base(message)
        {
        }
    }
}