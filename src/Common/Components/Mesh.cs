using System.Numerics;

namespace LearnDirectX.src.Common.Components
{
    public class Mesh : Component
    {
        public Vector4[] Vertexes;

        public Mesh() { }

        public Mesh(Vector4[] vertexes)
        {
            Vertexes = vertexes;
        }
    }
}
