using SharpDX.D3DCompiler;

namespace LearnDirectX.src.Common.EngineSystem.Shaders
{
    public class DepthShader : Shader
    {
        private SharpDX.Direct3D11.VertexShader _shader;

        public DepthShader(ShaderBytecode shaderByteCode) : base(shaderByteCode) { }

        public override void Initialize()
        {
            _shader = new SharpDX.Direct3D11.VertexShader(Window.Instance.Device, _shaderByteCode);
        }

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}
