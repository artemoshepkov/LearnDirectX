using LearnDirectX.src.Common.EngineSystem;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using System.Numerics;

namespace LearnDirectX.src.Common.Components
{
    public class MeshRenderer : Component
    {
        #region Fields

        private Shader _shader;
        private Mesh _mesh;
        private Buffer _vertexBuffer;
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
            _vertexBufferBinding = new VertexBufferBinding(_vertexBuffer, Utilities.SizeOf<Vector4>() * 2, 0);
        }
        public void Render()
        {
            var context = Window.Instance.Device.ImmediateContext;

            context.InputAssembler.InputLayout = _shader.InputLayout;
            context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            context.InputAssembler.SetVertexBuffers(0, _vertexBufferBinding);
            _shader.Use();

            context.Draw(3, 0);
        }

        #endregion
    }
}
