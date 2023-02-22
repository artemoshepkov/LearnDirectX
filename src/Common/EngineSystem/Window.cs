using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using SharpDX.Windows;
using System;
using System.Linq.Expressions;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
using Device1 = SharpDX.Direct3D11.Device1;

namespace LearnDirectX.src.Common.EngineSystem
{
    public sealed class Window : IDisposable
    {
        #region Fields

        private static Window _instance;

        private RenderForm _renderForm;
        private SwapChain1 _swapChain;
        public RenderTargetView _renderTargetView;
        private Texture2D _backBuffer;

        private bool _updateViewport = true;

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

        public RenderForm RenderForm { get => Instance._renderForm; }
        public Device1 Device { get; private set; }

        public RawColor4 BackGroundColor { get; private set; } = new RawColor4(0.8f, 0.8f, 0.8f, 1f);
        
        #endregion

        #region Constructor

        private Window() { }

        #endregion

        #region Public methods

        public static void Init(string title, int width, int height)
        {
            Instance._renderForm = new RenderForm
            {
                Text = title,
                Width = width,
                Height = height
            };

            Instance.InitializeDevice();
            Instance.InitializeSwapChain();

            Instance._backBuffer = Texture2D.FromSwapChain<Texture2D>(Instance._swapChain, 0);
            Instance._renderTargetView = new RenderTargetView(Instance.Device, Instance._backBuffer);

            Instance.Device.ImmediateContext.Rasterizer.State = new RasterizerState(Instance.Device, new RasterizerStateDescription
            {
                CullMode = CullMode.None,
                FillMode = SharpDX.Direct3D11.FillMode.Solid,
            });
            Instance.Device.ImmediateContext.Rasterizer.SetViewport(0, 0, Width, Height, 0f, 1f);
            Instance.Device.ImmediateContext.OutputMerger.SetTargets(Instance._renderTargetView);

            Engine.AddEventUpdate(Update); // Create another class for event
        }

        public static void UpdateWindow()
        {
            if (Instance._updateViewport)
            {
                Instance.Device.ImmediateContext.Rasterizer.SetViewport(0, 0, Instance._renderForm.Width, Instance._renderForm.Height, 0f, 1f);
                Instance._updateViewport = false;
            }
        }

        public static void Update()
        {
            Instance._swapChain.Present(0, PresentFlags.None, new PresentParameters());
        }

        public static void Clear()
        {
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
            //Instance._renderForm.Cursor.Hide() = true;
            Console.WriteLine("Change cursor mode");
        }

        public static void Exit()
        {
            Console.WriteLine("Exit app");
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
                    SampleDescription = new SampleDescription(1, 0),
                    Usage = Usage.BackBuffer | Usage.RenderTargetOutput,
                    BufferCount = 1,
                    Scaling = Scaling.Stretch,
                    SwapEffect = SwapEffect.Discard,
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
