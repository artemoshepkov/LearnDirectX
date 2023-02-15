using LearnDirectX.src.Common.EngineSystem;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System.Numerics;
using System.Runtime.Remoting.Contexts;

namespace LearnDirectX.src.Common.Components
{
    public class MeshRenderer : Component
    {
        #region Fields

        private Shader _shader;
        private Mesh _mesh;
        private Buffer _vertexBuffer;

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
        }
        public void Render()
        {
            var context = Window.Instance.Device.ImmediateContext;

            context.InputAssembler.InputLayout = _shader.InputLayout;
            context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(_vertexBuffer, 32, 0));
            _shader.Use();

            context.Draw(3, 0);
        }
        
        #endregion
    }
}
