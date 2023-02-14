using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using SharpDX.Windows;
using System;

using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
using Device1 = SharpDX.Direct3D11.Device1;

namespace LearnDirectX.src.Common.EngineSystem
{
    public class Window
    {
        #region Fields

        private static Window _instance;

        private RenderForm _window;
        private Device1 _device;
        private SwapChain1 _swapChain;
        private RenderTargetView _renderTargetView;
        private Texture2D _backBuffer;

        #endregion

        #region Properties

        public RawColor4 BackGroundColor { get; private set; } = new RawColor4(0.8f, 0.8f, 0.8f, 1f);
        
        static int Widht { get => _instance._window.Width; }
        static int Height { get => _instance._window.Height; }

        #endregion

        #region Constructor

        private Window() { }

        #endregion

        #region Public methods

        public static Window GetInstance()
        {
            if (_instance == null)
                _instance = new Window();
            return _instance;
        }

        public static void Init(string title, int width, int height)
        {
            _instance._window = new RenderForm
            {
                Text = title,
                Width = width,
                Height = height
            };

            _instance.InitializeDevice();
            _instance.InitializeSwapChain();

            _instance._backBuffer = Texture2D.FromSwapChain<Texture2D>(_instance._swapChain, 0);
            _instance._renderTargetView = new RenderTargetView(_instance._device, _instance._backBuffer);
        }

        public void UpdateWindow()
        {

        }

        public static void Clear()
        {
            _instance._device.ImmediateContext.ClearRenderTargetView(_instance._renderTargetView, _instance.BackGroundColor);
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
                    Width = _window.Width,
                    Height = _window.Height,
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
}
