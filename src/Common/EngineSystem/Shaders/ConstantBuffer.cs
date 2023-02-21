using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Runtime.InteropServices;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace LearnDirectX.src.Common.EngineSystem.Shaders
{
    public class ConstantBuffer<T> : IDisposable where T : struct
    {
        private readonly Device _device;
        private readonly Buffer _buffer;
        private readonly DataStream _dataStream;

        public Buffer Buffer => _buffer;

        public ConstantBuffer()
        {
            _device = Window.Instance.Device;

            int size = Marshal.SizeOf(typeof(T));

            _buffer = new Buffer(_device,
                new BufferDescription
                {
                    Usage = ResourceUsage.Default,
                    BindFlags = BindFlags.ConstantBuffer,
                    SizeInBytes = size,
                    CpuAccessFlags = CpuAccessFlags.None,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0,
                });

            _dataStream = new DataStream(size, true, true);
        }

        public void UpdateValue(T value)
        {
            Marshal.StructureToPtr(value, _dataStream.DataPointer, false);

            var dataBox = new DataBox(_dataStream.DataPointer, 0, 0);

            _device.ImmediateContext.UpdateSubresource(dataBox, _buffer, 0);
        }

        public void Dispose()
        {
            _dataStream?.Dispose();
            _buffer?.Dispose();
        }
    }
}
