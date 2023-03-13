using LearnDirectX.src.Common.Components;
using LearnDirectX.src.Common.EngineSystem.Rendering;

namespace LearnDirectX.src.Common.EngineSystem.Shaders.Uploaders
{
    public abstract class ShaderBufferUploader
    {
        protected readonly int _registerNumber;

        public ShaderBufferUploader(int registerNumber)
        {
            _registerNumber = registerNumber;
        }

        public abstract void Upload(GameObject gameObject, RendererContext rendererContext);
    }
}
