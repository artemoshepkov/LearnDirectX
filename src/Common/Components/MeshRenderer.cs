using LearnDirectX.src.Common.EngineSystem;
using LearnDirectX.src.Common.EngineSystem.Rendering;
using LearnDirectX.src.Common.EngineSystem.Shaders;
using LearnDirectX.src.Common.EngineSystem.Shaders.Structures;
using LearnDirectX.src.Common.EngineSystem.Shaders.Structures.Lights;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using System;
using System.Linq;
using System.Numerics;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace LearnDirectX.src.Common.Components
{
    public class MeshRenderer : Component
    {
        #region Fields

        private Shader[] _shaders;
        private Mesh _mesh;
        private Buffer _vertexBuffer;
        private Buffer _indexBuffer;
        private VertexBufferBinding _vertexBufferBinding;

        private ConstantBuffer<PerObject> _perObjectBuffer;
        private ConstantBuffer<PerFrame> _perFrameBuffer;

        #endregion

        #region Constructors

        public MeshRenderer() { }

        public MeshRenderer(Shader[] shader)
        {
            _shaders = shader;
        }

        #endregion

        #region Public methods

        public void Initialize()
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

            _perObjectBuffer = new ConstantBuffer<PerObject>();
            _perFrameBuffer = new ConstantBuffer<PerFrame>();

            Window.Instance.Device.ImmediateContext.VertexShader.SetConstantBuffer(0, _perObjectBuffer.Buffer);

            Window.Instance.Device.ImmediateContext.PixelShader.SetConstantBuffer(1, _perFrameBuffer.Buffer);
        }

        public void Render(RendererContext context)
        {
            var immediateContext = Window.Instance.Device.ImmediateContext;

            foreach (var shader in _shaders)
            {
                shader.Use();
            }

            immediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            immediateContext.InputAssembler.SetIndexBuffer(_indexBuffer, SharpDX.DXGI.Format.R16_UInt, 0);
            immediateContext.InputAssembler.SetVertexBuffers(0, _vertexBufferBinding);

            #region Load worldViewProjection

            var Model = Owner.GetComponent<Transform>().Model;
            var View = context.CameraContext.Camera.GetViewMatrix();
            var Projection = context.CameraContext.Camera.GetProjectionMatrix();

            var perObject = new PerObject()
            {
                ViewProjection = Matrix4x4.Multiply(View, Projection),
                Model = Model,
            };

            _perObjectBuffer.UpdateValue(perObject);

            #endregion


            #region Load PerFrame

            var light = context.Lights.First().GetComponent<DirectLight>();

            var perFrame = new PerFrame()
            {
                CameraPosition = context.CameraContext.Transform.Position,
                Light = new DirectionalLight()
                {
                    Color = light.Color,
                    Direction = light.Direction,
                },
            };

            _perFrameBuffer.UpdateValue(perFrame);

            #endregion

            immediateContext.DrawIndexed(_mesh.Indexes.Length, 0, 0);
        }

        #endregion
    }
}
