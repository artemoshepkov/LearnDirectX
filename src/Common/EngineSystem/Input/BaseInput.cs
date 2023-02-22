using SharpDX.DirectInput;

namespace LearnDirectX.src.Common.EngineSystem.Input
{
    public abstract class BaseInput
    {
        protected static DirectInput _directInput = new DirectInput();

        public abstract void Init();
        public abstract void Update();
    }
}
