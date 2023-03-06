using System.Numerics;

namespace LearnDirectX.src.Common.Components
{
    public class Material : Component
    {
        public Vector4 Color;

        public Material()
        {

        }

        public Material(Vector4 color)
        {
            Color = color;
        }

    }
}
