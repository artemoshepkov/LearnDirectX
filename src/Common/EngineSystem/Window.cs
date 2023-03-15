using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using System;
using Device = SharpDX.Direct3D11.Device;
using Device1 = SharpDX.Direct3D11.Device1;

namespace LearnDirectX.src.Common.EngineSystem
{
    public sealed class Window : IDisposable
    {
        #region Fields

        private static Window _instance;

        private Form _renderForm;
        private SwapChain1 _swapChain;

        public RenderTargetView _renderTargetView;
        private Texture2D _backBuffer;
        private Texture2D _depthBuffer;
        private DepthStencilView _depthView;

        private bool _updateViewport = true;
        private SampleDescription _sampleDescription;

        #endregion

        #region Properties

        public static Window Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Window();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public static int Width { get => Instance._renderForm.Width; }
        public static int Height { get => Instance._renderForm.Height; }

        public static bool IsCursorHide { get; private set; }

        public Form RenderForm { get => Instance._renderForm; }
        public Device1 Device { get; private set; }

        public RawColor4 BackGroundColor { get; private set; } = new RawColor4(0.8f, 0.8f, 0.8f, 1f);

        #endregion

        #region Constructor

        private Window() { }

        #endregion

        #region Public methods

        public static void Init(string title, int width, int height)
        {
            Instance._renderForm = new Form
            {
                Text = title,
                Width = width,
                Height = height
            };

            Instance._sampleDescription = new SampleDescription(8, 0);

            Instance.InitializeDevice();
            Instance.InitializeSwapChain();

            Instance._backBuffer = Texture2D.FromSwapChain<Texture2D>(Instance._swapChain, 0);
            Instance._renderTargetView = new RenderTargetView(Instance.Device, Instance._backBuffer);
            Instance._depthBuffer = new Texture2D(
                Instance.Device,
                new Texture2DDescription
                {
                    Format = Format.D32_Float_S8X24_UInt,
                    ArraySize = 1,
                    MipLevels = 1,
                    Width = Width,
                    Height = Height,
                    SampleDescription = Instance._sampleDescription,
                    Usage = ResourceUsage.Default,
                    BindFlags = BindFlags.DepthStencil,
                    CpuAccessFlags = CpuAccessFlags.None,
                    OptionFlags = ResourceOptionFlags.None,

                });

            Instance._depthView = new DepthStencilView(Instance.Device, Instance._depthBuffer);

            Instance.Device.ImmediateContext.Rasterizer.State = new RasterizerState(Instance.Device, new RasterizerStateDescription
            {
                CullMode = CullMode.None,
                FillMode = FillMode.Solid,
                IsAntialiasedLineEnabled = true,
                IsMultisampleEnabled = true,
            });
            Instance.Device.ImmediateContext.Rasterizer.SetViewport(0, 0, Width, Height);
            Instance.Device.ImmediateContext.OutputMerger.SetTargets(Instance._depthView, Instance._renderTargetView);

            IsCursorHide = false;
            ChangeCursorMode();

            Engine.AddEventUpdate(Update);
        }

        public static void UpdateWindow()
        {
            if (Instance._updateViewport)
            {
                Instance.Device.ImmediateContext.Rasterizer.SetViewport(0, 0, Width, Height);
                Instance._updateViewport = false;
            }
        }

        public static void Update()
        {
            Instance._swapChain.Present(0, PresentFlags.None, new PresentParameters());
        }

        public static void Clear()
        {
            Instance.Device.ImmediateContext.ClearDepthStencilView(Instance._depthView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1f, 0);
            Instance.Device.ImmediateContext.ClearRenderTargetView(Instance._renderTargetView, Instance.BackGroundColor);
        }

        public void Dispose()
        {
            Device.Dispose();
            _swapChain.Dispose();
            _renderTargetView.Dispose();
            _backBuffer.Dispose();
        }

        public static void ChangeCursorMode()
        {
            IsCursorHide = !IsCursorHide;
            Instance._renderForm.HideCursor(IsCursorHide);
        }

        public static void Exit()
        {
            Instance.RenderForm.Close();
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
                Device = device11.QueryInterfaceOrNull<Device1>();
                if (Device == null)
                {
                    throw new NotSupportedException("SharpDX.Derice3D11.Device1 is not supported");
                }
            }
        }

        private void InitializeSwapChain()
        {
            using (var dxgi = Device.QueryInterface<SharpDX.DXGI.Device2>())
            using (var adapter = dxgi.Adapter)
            using (var factory = adapter.GetParent<Factory2>())
            {
                var desc1 = new SwapChainDescription1()
                {
                    Width = _renderForm.Width,
                    Height = _renderForm.Height,
                    Format = Format.R8G8B8A8_UNorm,
                    Stereo = false,
                    SampleDescription = Instance._sampleDescription,
                    Usage = Usage.BackBuffer | Usage.RenderTargetOutput,
                    BufferCount = 1,
                    Scaling = Scaling.Stretch,
                    SwapEffect = SwapEffect.Discard,
                    Flags = SwapChainFlags.AllowModeSwitch
                };

                _swapChain = new SwapChain1(
                    factory,
                    Device,
                    _renderForm.Handle,
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
}
