using System.Numerics;
using System.Runtime.InteropServices;

namespace LearnDirectX.src.Common.EngineSystem.Rendering
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PerObject
    {
        public Matrix4x4 WorldViewProjection;
    }
}
