using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using SharpDX.Windows;
using System;
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

        public int Width { get; private set; }
        public int Height { get; private set; }
        public RawColor4 BackGroundColor { get; private set; } = new RawColor4(0.8f, 0.8f, 0.8f, 1f);

        #endregion

        #region Constructors

        public Application(string title, int width, int height)
        {
            Width= width;
            Height= height;

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
            var core = new Application("Game", 640, 480);

            core.Run();

            core.Dispose();
        }
    }
}
