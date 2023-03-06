using System.Numerics;
using System.Runtime.InteropServices;

namespace LearnDirectX.src.Common.EngineSystem.Shaders.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PerMaterial
    {
        public Material Mat;
    }
}
