using LearnDirectX.src.Common.Components;
using System.Collections.Generic;

namespace LearnDirectX.src.Common.EngineSystem.Rendering
{
    public struct RendererContext
    {
        public CameraContext CameraContext;
        public List<GameObject> Lights;
    }
}
