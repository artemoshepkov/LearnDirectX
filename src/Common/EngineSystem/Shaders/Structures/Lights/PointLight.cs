using System.Numerics;
using System.Runtime.InteropServices;

namespace LearnDirectX.src.Common.EngineSystem.Shaders.Structures.Lights
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PointLight
    {
        public Vector4 Color;
        public Vector3 Position;
        float _padding0;
        public Attenuation Attenuation;
    }
}
