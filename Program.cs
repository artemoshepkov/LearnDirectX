using LearnDirectX.src.Common.Components;
using LearnDirectX.src.Common.EngineSystem;
using LearnDirectX.src.Common.EngineSystem.Rendering;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using SharpDX.Windows;
using System;
using System.Numerics;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
using Device1 = SharpDX.Direct3D11.Device1;

namespace LearnDirectX
{
    public class Application : IDisposable
    {
        #region Private variables

        RenderForm _window;

        Device1 _device;
        SwapChain1 _swapChain;
        RenderTargetView _renderTargetView;

        Texture2D _backBuffer;

        #endregion

        #region Properties

        public readonly string ShadersPath = "../../src/Shaders/";

        public int Width { get; private set; }
        public int Height { get; private set; }
        public RawColor4 BackGroundColor { get; private set; } = new RawColor4(0.8f, 0.8f, 0.8f, 1f);

        #endregion

        #region Constructors

        public Application(string title, int width, int height)
        {
            #region App init

            Width = width;
            Height = height;

            _window = new RenderForm()
            {
                Text = "Game",
                Width = Width,
                Height = Height,
            };

            InitializeDevice();

            InitializeSwapChain();

            _backBuffer = Texture2D.FromSwapChain<Texture2D>(_swapChain, 0);
            _renderTargetView = new RenderTargetView(_device, _backBuffer);

            #endregion

            #region Shaders

            var context = _device.ImmediateContext;

            var vertexShaderByteCode = ShaderBytecode.CompileFromFile($"{ShadersPath}vertex.hlsl", "VS", "vs_4_0", ShaderFlags.None, EffectFlags.None);
            var vertexShader = new VertexShader(_device, vertexShaderByteCode);

            var pixelShaderByteCode = ShaderBytecode.CompileFromFile($"{ShadersPath}pixel.hlsl", "PS", "ps_4_0", ShaderFlags.None, EffectFlags.None);
            var pixelShader = new PixelShader(_device, pixelShaderByteCode);



            var layout = new InputLayout(
                _device,
                ShaderSignature.GetInputSignature(vertexShaderByteCode),
                new[]
                {
                    new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                    new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0),
                }
                );

            var vertices = Buffer.Create(_device, BindFlags.VertexBuffer, new[]
                {
                    new Vector4(0.0f, 0.5f, 0.5f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                    new Vector4(0.5f, -0.5f, 0.5f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                    new Vector4(-0.5f, -0.5f, 0.5f, 1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f)
                });

            context.InputAssembler.InputLayout = layout;
            context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertices, 32, 0));
            context.VertexShader.Set(vertexShader);
            context.Rasterizer.State = new RasterizerState(_device, new RasterizerStateDescription
            {
                CullMode = CullMode.None,
                FillMode = FillMode.Solid,
            });
            context.Rasterizer.SetViewport(0, 0, Width, Height, 0f, 1f);
            context.PixelShader.Set(pixelShader);
            context.OutputMerger.SetTargets(_renderTargetView);

            #endregion
        }

        #endregion

        #region Public methods

        public void Run()
        {
            RenderLoop.Run(_window, RenderCallback);
        }

        public void RenderCallback()
        {
            _device.ImmediateContext.ClearRenderTargetView(_renderTargetView, BackGroundColor);

            _device.ImmediateContext.Draw(3, 0);

            _swapChain.Present(0, PresentFlags.None, new PresentParameters());
        }

        public void Dispose()
        {
            _renderTargetView.Dispose();
            _backBuffer.Dispose();
            _device.Dispose();
            _swapChain.Dispose();
        }

        #endregion

        #region Private methods

        private void InitializeDevice()
        {
            using (var device11 = new Device(
                SharpDX.Direct3D.DriverType.Hardware,
                DeviceCreationFlags.None,
                new[]
                {
                                            SharpDX.Direct3D.FeatureLevel.Level_11_1,
                                            SharpDX.Direct3D.FeatureLevel.Level_11_0,
                }
                ))
            {
                _device = device11.QueryInterfaceOrNull<Device1>();
                if (_device == null)
                {
                    throw new NotSupportedException("SharpDX.Derice3D11.Device1 is not supported");
                }
            }
        }

        private void InitializeSwapChain()
        {
            using (var dxgi = _device.QueryInterface<SharpDX.DXGI.Device2>())
            using (var adapter = dxgi.Adapter)
            using (var factory = adapter.GetParent<Factory2>())
            {
                var desc1 = new SwapChainDescription1()
                {
                    Width = Width,
                    Height = Height,
                    Format = Format.R8G8B8A8_UNorm,
                    Stereo = false,
                    SampleDescription = new SampleDescription(1, 0),
                    Usage = Usage.BackBuffer | Usage.RenderTargetOutput,
                    BufferCount = 1,
                    Scaling = Scaling.Stretch,
                    SwapEffect = SwapEffect.Discard,
                };

                _swapChain = new SwapChain1(
                    factory,
                    _device,
                    _window.Handle,
                    ref desc1,
                    new SwapChainFullScreenDescription()
                    {
                        RefreshRate = new Rational(60, 1),
                        Scaling = DisplayModeScaling.Centered,
                        Windowed = true,
                    },
                    null);
            }
        }

        #endregion
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Engine.Init("Game", 640, 480);

            Engine.GetInstance().AddRenderLayer(new DirectXSceneRenderer(InitializeScene()));

            Engine.Run();
        }

        static Scene InitializeScene()
        {
            string ShadersPath = "../../src/Shaders/";

            var scene = new Scene();
            GameObject gObj = new GameObject();

            gObj.AddComponent(new Mesh(
                new Vector4[]
                {
                    new Vector4(0.0f, 0.5f, 0.5f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                    new Vector4(0.5f, -0.5f, 0.5f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                    new Vector4(-0.5f, -0.5f, 0.5f, 1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f)
                }));
            var vsbc = ShaderBytecode.CompileFromFile($"{ShadersPath}vertex.hlsl", "VS", "vs_4_0", ShaderFlags.None, EffectFlags.None);
            var psbc = ShaderBytecode.CompileFromFile($"{ShadersPath}pixel.hlsl", "PS", "ps_4_0", ShaderFlags.None, EffectFlags.None);
            gObj.AddComponent(new MeshRenderer(new Shader(vsbc, psbc)));
            gObj.GetComponent<MeshRenderer>().Initialize();

            scene.AddObject(gObj);

            return scene;
        }
    }
}
