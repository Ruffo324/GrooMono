using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrooMono.Games.PinguRush.Exceptions
{
    public class ObstacleException : Exception
    {
        public ObstacleException(string message) : base(message)
        {
        }
    }
}
