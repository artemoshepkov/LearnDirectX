using LearnDirectX.src.Common.EngineSystem;
using LearnDirectX.src.Common.EngineSystem.Rendering;
using LearnDirectX.src.Common.EngineSystem.Shaders;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using System.Numerics;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace LearnDirectX.src.Common.Components
{
    public class MeshRenderer : Component
    {
        #region Fields

        private Shader _shader;
        private Mesh _mesh;
        private Buffer _vertexBuffer;
        private Buffer _indexBuffer;
        private Buffer _constVertexBuffer;
        private VertexBufferBinding _vertexBufferBinding;

        #endregion

        #region Constructors

        public MeshRenderer() { }

        public MeshRenderer(Shader shader)
        {
            _shader = shader;
        }

        #endregion

        #region Public methods

        public void Initialize()
        {
            _shader.Initialize();
            _mesh = Owner.GetComponent<Mesh>();
            GenBuffers();
        }

        public void GenBuffers()
        {
            _vertexBuffer = Buffer.Create(
                Window.Instance.Device,
                BindFlags.VertexBuffer,
                _mesh.Vertexes);

            _indexBuffer = Buffer.Create(
                Window.Instance.Device,
                BindFlags.IndexBuffer,
                _mesh.Indexes);

            _vertexBufferBinding = new VertexBufferBinding(_vertexBuffer, Utilities.SizeOf<Vertex>(), 0);

            _constVertexBuffer = new Buffer(Window.Instance.Device, Utilities.SizeOf<Matrix4x4>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);

            Window.Instance.Device.ImmediateContext.VertexShader.SetConstantBuffer(0, _constVertexBuffer);
        }

        public void Render(RendererContext context)
        {
            var immediateContext = Window.Instance.Device.ImmediateContext;

            _shader.Use();

            #region Load worldViewProjection

            var Model = Owner.GetComponent<Transform>().Model;
            var View = context.CameraContext.Camera.GetViewMatrix();
            var Projection = context.CameraContext.Camera.GetProjectionMatrix();

            var worldViewProjection = Matrix4x4.Transpose(Model * View * Projection);

            immediateContext.UpdateSubresource(ref worldViewProjection, _constVertexBuffer);

            #endregion

            immediateContext.InputAssembler.InputLayout = _shader.InputLayout;
            immediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            immediateContext.InputAssembler.SetIndexBuffer(_indexBuffer, SharpDX.DXGI.Format.R16_UInt, 0);
            immediateContext.InputAssembler.SetVertexBuffers(0, _vertexBufferBinding);

            immediateContext.DrawIndexed(_mesh.Indexes.Length, 0, 0);
        }

        #endregion
    }
}
