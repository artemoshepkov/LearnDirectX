using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace LearnDirectX.src.Common.EngineSystem.Shaders.Buffers
{
    public class ConstantBuffer<T> : IDisposable where T : struct
    {
        public Buffer Buffer { get; }

        public ConstantBuffer()
        {
            int size = Marshal.SizeOf(typeof(T));

            Buffer = new Buffer(
                Window.Instance.Device,
                new BufferDescription
                {
                    Usage = ResourceUsage.Default,
                    BindFlags = BindFlags.ConstantBuffer,
                    SizeInBytes = size,
                    CpuAccessFlags = CpuAccessFlags.None,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0,
                });
        }

        public void UpdateValue(T value)
        {
            Window.Instance.Device.ImmediateContext.UpdateSubresource(ref value, Buffer);
        }

        public void Dispose()
        {
            Buffer?.Dispose();
        }
    }
}
