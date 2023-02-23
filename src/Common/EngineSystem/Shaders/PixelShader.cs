using SharpDX.D3DCompiler;

namespace LearnDirectX.src.Common.EngineSystem.Shaders
{
    public class PixelShader : Shader
    {
        private SharpDX.Direct3D11.PixelShader _shader;

        public PixelShader(ShaderBytecode shaderByteCode) : base(shaderByteCode)
        { 
        }

        public override void Initialize()
        {
            _shader = new SharpDX.Direct3D11.PixelShader(Window.Instance.Device, _shaderByteCode);
        }

        public override void Use()
        {
            var context = Window.Instance.Device.ImmediateContext;
            if (context == null)
            {
                return;
            }

            context.PixelShader.Set(_shader);
        }

        public new void Dispose()
        {
            base.Dispose();
            _shader.Dispose();
        }
    }
}
