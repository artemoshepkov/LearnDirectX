using System.Numerics;
using System.Runtime.InteropServices;

namespace LearnDirectX.src.Common.EngineSystem.Shaders.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PerObject
    {
        public Matrix4x4 World;
        public Matrix4x4 WorldInverseTranspose;
        public Matrix4x4 WorldViewProjection; // change to ViewProjection

        internal void Transpose()
        {
            World = Matrix4x4.Transpose(World);
            WorldInverseTranspose = Matrix4x4.Transpose(WorldInverseTranspose);
            WorldViewProjection = Matrix4x4.Transpose(WorldViewProjection);
        }
    }
}
