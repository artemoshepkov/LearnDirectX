using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;

namespace LearnDirectX.src.Common.EngineSystem
{
    public sealed class Shader : IDisposable
    {
        private VertexShader _vertexShader;
        private PixelShader _pixelShader;

        public readonly ShaderBytecode VertexShaderByteCode;
        public readonly ShaderBytecode PixelShaderByteCode;
        public readonly InputLayout InputLayout;

        public Shader(ShaderBytecode vertexShaderByteCode, ShaderBytecode pixelShaderByteCode)
        {
            VertexShaderByteCode = vertexShaderByteCode;
            PixelShaderByteCode = pixelShaderByteCode;

            InputLayout = new InputLayout(
                Window.Instance.Device,
                ShaderSignature.GetInputSignature(VertexShaderByteCode),
                    new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0),
                    }
                );
        }

        public void Initialize()
        {
            var device = Window.Instance.Device;
            _vertexShader = new VertexShader(device, VertexShaderByteCode);
            _pixelShader = new PixelShader(device, PixelShaderByteCode);
        }

        public void Use()
        {
            var context = Window.Instance.Device.ImmediateContext;
            if (context == null)
            {
                return;
            }

            context.VertexShader.Set(_vertexShader);
            context.PixelShader.Set(_pixelShader);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
