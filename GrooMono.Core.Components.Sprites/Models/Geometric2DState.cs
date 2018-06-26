using System.Drawing;

namespace GrooMono.Core.Components.Sprites.Models
{
    public class Geometric2DState
    {
        public float Rotation;
        public float X;
        public float Y;

        public Geometric2DState(float x, float y, float rotation = 0f)
        {
            X = x;
            Y = y;
            Rotation = rotation;
        }

        public Geometric2DState(Size size, float rotation = 0f)
        {
            X = size.Width;
            Y = size.Height;
            Rotation = rotation;
        }
    }
}