using LearnDirectX.src.Common.EngineSystem.Shaders;
using System.Numerics;

namespace LearnDirectX.src.Common.Components
{
    public class Mesh : Component
    {
        public Vertex[] Vertexes;
        public ushort[] Indexes;

        public Mesh() { }

        public Mesh(Vertex[] vertexes, ushort[] indexes)
        {
            Vertexes = vertexes;
            Indexes = indexes;
        }
    }
}
