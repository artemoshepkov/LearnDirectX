using LearnDirectX.src.Common.Components;
using LearnDirectX.src.Common.EngineSystem.Rendering;
using LearnDirectX.src.Common.EngineSystem.Shaders.Buffers;
using LearnDirectX.src.Common.EngineSystem.Shaders.Structures;
using static DevExpress.XtraEditors.RoundedSkinPanel;

namespace LearnDirectX.src.Common.EngineSystem.Shaders.Uploaders
{
    public sealed class MaterialUploader : ShaderBufferUploader
    {
        private ConstantBuffer<PerMaterial> _perMaterialBuffer;

        public MaterialUploader(int registerNum) : base(registerNum)
        {
            _perMaterialBuffer = new ConstantBuffer<PerMaterial>();
        }
        public override void Upload(GameObject gameObject, RendererContext rendererContext)
        {
            var immediateContext = Window.Instance.Device.ImmediateContext;

            immediateContext.VertexShader.SetConstantBuffer(_registerNumber, _perMaterialBuffer.Buffer); 

            var material = gameObject.GetComponent<Components.Material>();

            var perMaterial = new PerMaterial()
            {
                Mat = new Structures.Material()
                {
                    Color = material.Color,
                },
            };

            _perMaterialBuffer.UpdateValue(perMaterial);
        }
    }
}
