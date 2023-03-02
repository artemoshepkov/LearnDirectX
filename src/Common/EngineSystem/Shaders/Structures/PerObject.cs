using System.Numerics;
using System.Runtime.InteropServices;

namespace LearnDirectX.src.Common.EngineSystem.Shaders.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PerObject
    {
        public Matrix4x4 ViewProjection;
        public Matrix4x4 Model;
        public Matrix4x4 WorldInverseTranspose;
    }
}
