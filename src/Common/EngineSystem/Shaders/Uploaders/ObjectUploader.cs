using LearnDirectX.src.Common.Components;
using LearnDirectX.src.Common.EngineSystem.Rendering;
using LearnDirectX.src.Common.EngineSystem.Shaders.Buffers;
using LearnDirectX.src.Common.EngineSystem.Shaders.Structures;
using static DevExpress.XtraEditors.RoundedSkinPanel;
using System.Numerics;
using System.Runtime.Remoting.Contexts;

namespace LearnDirectX.src.Common.EngineSystem.Shaders.Uploaders
{
    public sealed class ObjectUploader : ShaderBufferUploader
    {
        private ConstantBuffer<PerObject> _perObjectBuffer;

        public ObjectUploader(int registerNumber) : base(registerNumber)
        {
            _perObjectBuffer = new ConstantBuffer<PerObject>();
        }

        public override void Upload(GameObject gameObject, RendererContext rendererContext)
        {
            var immediateContext = Window.Instance.Device.ImmediateContext;

            immediateContext.VertexShader.SetConstantBuffer(_registerNumber, _perObjectBuffer.Buffer);

            var Model = gameObject.GetComponent<Transform>().Model;
            var View = rendererContext.CameraContext.Camera.GetViewMatrix();
            var Projection = rendererContext.CameraContext.Camera.GetProjectionMatrix();

            Matrix4x4 InverseTransposeModel;
            Matrix4x4.Invert(Model, out InverseTransposeModel);
            InverseTransposeModel = Matrix4x4.Transpose(InverseTransposeModel);

            var perObject = new PerObject()
            {
                ViewProjection = Matrix4x4.Multiply(View, Projection),
                Model = Model,
                WorldInverseTranspose = InverseTransposeModel,
            };

            _perObjectBuffer.UpdateValue(perObject);
        }
    }
}
