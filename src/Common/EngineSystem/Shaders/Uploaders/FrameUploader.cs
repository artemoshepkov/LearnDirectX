using LearnDirectX.src.Common.Components;
using LearnDirectX.src.Common.EngineSystem.Rendering;
using LearnDirectX.src.Common.EngineSystem.Shaders.Buffers;
using LearnDirectX.src.Common.EngineSystem.Shaders.Structures;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;

namespace LearnDirectX.src.Common.EngineSystem.Shaders.Uploaders
{
    public sealed class FrameUploader : ShaderBufferUploader
    {
        private ConstantBuffer<PerFrame> _buffer;

        public FrameUploader(int registerNumber) : base(registerNumber)
        {
            _buffer = new ConstantBuffer<PerFrame>();
        }

        public override void Upload(GameObject gameObject, RendererContext rendererContext)
        {
            var immediateContext = Window.Instance.Device.ImmediateContext;

            immediateContext.PixelShader.SetConstantBuffer(_registerNumber, _buffer.Buffer);

            var perFrame = new PerFrame()
            {
                CameraPosition = rendererContext.CameraContext.Transform.Position,
            };

            _buffer.UpdateValue(perFrame);
        }
    }
}
