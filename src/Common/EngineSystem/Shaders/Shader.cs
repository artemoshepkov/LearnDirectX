using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace LearnDirectX.src.Common.EngineSystem
{
    public abstract class Shader : IDisposable
    {
        protected readonly ShaderBytecode _shaderByteCode;

        public Shader(ShaderBytecode shaderByteCode)
        {
            _shaderByteCode = shaderByteCode;
        }

        public abstract void Initialize();

        public abstract void Use();

        public void Dispose()
        {
            _shaderByteCode.Dispose();
        }
    }
}
