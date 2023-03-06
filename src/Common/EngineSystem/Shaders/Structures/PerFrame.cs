using LearnDirectX.src.Common.EngineSystem.Shaders.Structures.Lights;
using System.Numerics;
using System.Runtime.InteropServices;

namespace LearnDirectX.src.Common.EngineSystem.Shaders.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PerFrame
    {
        public Vector3 CameraPosition;
        private float _padding0;
        public DirectionalLight DirectLight;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public PointLight[] PointLights;
    }
}
