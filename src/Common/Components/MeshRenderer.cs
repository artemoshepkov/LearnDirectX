using LearnDirectX.src.Common.EngineSystem;
using LearnDirectX.src.Common.EngineSystem.Rendering;
using LearnDirectX.src.Common.EngineSystem.Shaders.Uploaders;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;
using LearnDirectX.src.Common.EngineSystem.Shaders;

namespace LearnDirectX.src.Common.Components
{
    public class MeshRenderer : Component
    {
        #region Fields

        private Shader[] _shaders;
        private ShaderBufferUploader[] _shadersUploader;
        private Mesh _mesh;
        private Buffer _vertexBuffer;
        private Buffer _indexBuffer;
        private VertexBufferBinding _vertexBufferBinding;

        #endregion

        #region Constructors

        public MeshRenderer() { }

        public MeshRenderer(Shader[] shader, ShaderBufferUploader[] shadersUploader)
        {
            _shaders = shader;
            _shadersUploader = shadersUploader;
        }

        #endregion

        #region Public methods

        public override void Start()
        {
            foreach (var shader in _shaders)
            {
                shader.Initialize();
            }
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
        }

        public void Render(RendererContext context)
        {
            foreach (var shader in _shaders)
            {
                shader.Use();
            }

            foreach (var uploader in _shadersUploader)
            {
                uploader.Upload(Owner, context);
            }

            var immediateContext = Window.Instance.Device.ImmediateContext;
            immediateContext.InputAssembler.PrimitiveTopology = _mesh.PrimitiveTopology;
            immediateContext.InputAssembler.SetIndexBuffer(_indexBuffer, SharpDX.DXGI.Format.R16_UInt, 0);
            immediateContext.InputAssembler.SetVertexBuffers(0, _vertexBufferBinding);

            immediateContext.DrawIndexed(_mesh.Indexes.Length, 0, 0);

            immediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
        }

        #endregion
    }
}
