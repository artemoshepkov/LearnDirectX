namespace LearnDirectX.src.Common.EngineSystem.Rendering
{
    public class RenderLayer
    {
        public bool Enabled { get; private set; } = true;

        public void SetEnabled(bool status) => Enabled = status;

        public virtual void OnBegin() { }
        public virtual void OnEnd() { }
        public virtual void OnDraw() { }
    }
}
