using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System.Numerics;
using System.Runtime.InteropServices;

namespace LearnDirectX.src.Common.EngineSystem.Shaders
{
    public class VertexShader : Shader
    {
        private SharpDX.Direct3D11.VertexShader _shader;

        public readonly InputLayout InputLayout;

        public VertexShader(ShaderBytecode shaderByteCode) : base(shaderByteCode)
        {
            InputLayout = new InputLayout(
                Window.Instance.Device,
                ShaderSignature.GetInputSignature(_shaderByteCode),
                    new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("NORMAL", 0, Format.R32G32B32A32_Float, Marshal.SizeOf<Vector3>(), 0),
                    }
                );
        }

        public override void Initialize()
        {
            _shader = new SharpDX.Direct3D11.VertexShader(Window.Instance.Device, _shaderByteCode);
        }

        public override void Use()
        {
            var context = Window.Instance.Device.ImmediateContext;
            if (context == null)
            {
                return;
            }

            context.VertexShader.Set(_shader);
            Window.Instance.Device.ImmediateContext.InputAssembler.InputLayout = InputLayout;
        }

        public new void Dispose()
        {
            base.Dispose();
            _shader.Dispose();
            InputLayout.Dispose();
        }
    }
}
