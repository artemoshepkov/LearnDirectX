using System.Runtime.InteropServices;

namespace LearnDirectX.src.Common.EngineSystem.Shaders.Structures.Lights
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Attenuation
    {
        public float Constant;
        public float Linear;
        public float Quadratic;
        private float _padding0;

        public Attenuation(float constant, float linear, float quadratic)
        {
            Constant = constant;
            Linear = linear;
            Quadratic = quadratic;
            _padding0 = 0;
        }
    }
}
