using System.Collections.Generic;

namespace LearnDirectX.src.Common.EngineSystem.Rendering
{
    public sealed class RenderLayersSet
    {
        public List<RenderLayer> Layers { get; private set; } = new List<RenderLayer>();

        public void Render()
        {
            foreach (var layer in Layers)
            {
                if (!layer.Enabled)
                    continue;

                layer.OnBegin();
                layer.OnDraw();
                layer.OnEnd();
            }
        }

        public void Add(RenderLayer layer) => Layers.Add(layer);
    }
}
