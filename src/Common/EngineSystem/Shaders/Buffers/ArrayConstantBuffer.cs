using SharpDX.Direct3D11;
using System;
using System.Runtime.InteropServices;

namespace LearnDirectX.src.Common.EngineSystem.Shaders.Buffers
{
    public class ArrayConstantBuffer<T> : IDisposable where T : struct
    {
        public SharpDX.Direct3D11.Buffer Buffer { get; }

        public ArrayConstantBuffer(int countLigths)
        {
            int size = Marshal.SizeOf(typeof(T)) * countLigths;

            Buffer = new SharpDX.Direct3D11.Buffer(
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

        public void UpdateArray(T[] data)
        {
            Window.Instance.Device.ImmediateContext.UpdateSubresource(data, Buffer);
        }

        public void Dispose()
        {
            Buffer?.Dispose();
        }
    }
}
