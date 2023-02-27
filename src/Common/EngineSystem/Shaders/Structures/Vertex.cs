using System.Numerics;
using System.Runtime.InteropServices;

namespace LearnDirectX.src.Common.EngineSystem.Shaders
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector4 Color;

        public Vertex(Vector3 position, Vector3 normal, Vector4 color)
        {
            Position = position;
            Normal = normal;
            Color = color;
        }

        public Vertex(Vector3 position, Vector3 color) : this(position, Vector3.Normalize(position), new Vector4(color, 1f)) { }

        public Vertex(Vector3 position, Vector4 color) : this(position, Vector3.Normalize(position), color) { }

        public Vertex(Vector3 position) : this(position, Vector3.Normalize(position), new Vector4(1f, 1f, 1f, 1f)) { }

    }
}
