using LearnDirectX.src.Common.EngineSystem;
using LearnDirectX.src.Common.EngineSystem.Rendering;
using LearnDirectX.src.Common.EngineSystem.Shaders;
using LearnDirectX.src.Common.EngineSystem.Shaders.Structures;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using System.Numerics;
using LearnDirectX.src.Common.EngineSystem.Shaders.Structures.Lights;
using Buffer = SharpDX.Direct3D11.Buffer;
using VertexShader = LearnDirectX.src.Common.EngineSystem.Shaders.VertexShader;
using System;

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
        private ConstantBuffer<PerMaterial> _perMaterialBuffer;

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
            _perMaterialBuffer = new ConstantBuffer<PerMaterial>();

            Window.Instance.Device.ImmediateContext.VertexShader.SetConstantBuffer(0, _perObjectBuffer.Buffer);
            Window.Instance.Device.ImmediateContext.VertexShader.SetConstantBuffer(1, _perFrameBuffer.Buffer);
            Window.Instance.Device.ImmediateContext.VertexShader.SetConstantBuffer(2, _perMaterialBuffer.Buffer);

            Window.Instance.Device.ImmediateContext.PixelShader.SetConstantBuffer(1, _perFrameBuffer.Buffer);
            Window.Instance.Device.ImmediateContext.PixelShader.SetConstantBuffer(2, _perMaterialBuffer.Buffer);
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
                WorldViewProjection = Matrix4x4.Transpose(Model * View * Projection),
            };

            _perObjectBuffer.UpdateValue(perObject);

            #endregion


            #region Load PerFrame

            var perFrame = new PerFrame()
            {
                CameraPosition = context.CameraContext.Transform.Position,
                Light = new DirectionalLight()
                {
                    Color = new Vector4(1f),
                    Direction = new Vector3(1f, -1f, -1f),
                },
            };

            _perFrameBuffer.UpdateValue(perFrame);

            #endregion

            #region Load PerMaterial

            var perMaterial = new PerMaterial()
            {
                Ambient = new Vector4(0.2f),
                Diffuse = new Vector4(0.7f),
                Specular = new Vector4(1f),
                Shininess = 20f,
            };

            _perMaterialBuffer.UpdateValue(perMaterial);

            #endregion

            immediateContext.DrawIndexed(_mesh.Indexes.Length, 0, 0);
        }

        #endregion
    }
}
