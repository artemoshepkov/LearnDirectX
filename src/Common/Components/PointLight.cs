using System.Numerics;

namespace LearnDirectX.src.Common.Components
{
    public class PointLight : Component
    {
        public Vector4 Color;
        public EngineSystem.Shaders.Structures.Lights.Attenuation Attenuation;

        public PointLight() { }


    }
}
