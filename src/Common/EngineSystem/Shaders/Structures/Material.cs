using System.Numerics;
using System.Runtime.InteropServices;

namespace LearnDirectX.src.Common.EngineSystem.Shaders.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Material
    {
        public Vector3 Ambient;
        float _padding0;
        public Vector3 Diffuse;
        float _padding1;
        public Vector3 Specular;
        float _padding2;
    }
}
