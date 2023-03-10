using LearnDirectX.src.Common.EngineSystem.Shaders;
using SharpDX.Direct3D;
using System.Numerics;

namespace LearnDirectX.src.Common.Components
{
    public class Mesh : Component
    {
        public Vertex[] Vertexes;
        public ushort[] Indexes;
        public PrimitiveTopology PrimitiveTopology;

        public Mesh() { }

        public Mesh(Vertex[] vertexes, ushort[] indexes, PrimitiveTopology primitiveTopology = PrimitiveTopology.TriangleList)
        {
            Vertexes = vertexes;
            Indexes = indexes;
            PrimitiveTopology = primitiveTopology;
        }
    }
}
