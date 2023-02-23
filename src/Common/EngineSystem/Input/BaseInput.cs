using SharpDX.DirectInput;
using System;

namespace LearnDirectX.src.Common.EngineSystem.Input
{
    public abstract class BaseInput : IDisposable
    {
        protected static DirectInput _directInput = new DirectInput();

        public void Dispose()
        {
            _directInput?.Dispose();
            _directInput = null;
        }

        public abstract void Init();
        public abstract void Update();
    }
}
