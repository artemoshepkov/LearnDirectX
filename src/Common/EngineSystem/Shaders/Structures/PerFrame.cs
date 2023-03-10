using LearnDirectX.src.Common.EngineSystem.Shaders.Structures.Lights;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LearnDirectX.src.Common.EngineSystem.Shaders.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PerFrame
    {
        public Vector3 CameraPosition;
        private float _padding0;
        public DirectionalLight DirectLight;
        public PointLight PointLights;
    }
}
