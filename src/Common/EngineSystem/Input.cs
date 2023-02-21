using SharpDX.DirectInput;
using System;

namespace LearnDirectX.src.Common.EngineSystem
{
    public sealed class Input : IDisposable
    {
        private static Input _instance;

        private DirectInput _directInput;

        private Keyboard _keyBoard;
        private KeyboardState _keyBoardState;

        private Mouse _mouse;
        private MouseState _mouseState;

        private int _screenWidth;
        private int _screenHeight;

        public int MouseX
        {
            get;
            private set;
        }
        public int MouseY
        {
            get;
            private set;
        }
        private Input() { }

        public static Input Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Input();
                    _instance.Init();
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public void Init()
        {
            _screenHeight = Window.Height;
            _screenWidth = Window.Width;

            MouseX = Instance.MouseY = 0;

            _directInput = new DirectInput();

            _keyBoard = new Keyboard(_directInput);
            _keyBoard.Properties.BufferSize = 256;
            _keyBoard.SetCooperativeLevel(Window.Instance.RenderForm.Handle, CooperativeLevel.Foreground | CooperativeLevel.NonExclusive);

            _keyBoard.Acquire(); // danger place

            _mouse = new Mouse(_directInput);
            _mouse.Properties.AxisMode = DeviceAxisMode.Relative;
            _mouse.SetCooperativeLevel(Window.Instance.RenderForm.Handle, CooperativeLevel.Foreground | CooperativeLevel.NonExclusive);

            _mouse.Acquire();
        }

        public void Dispose()
        {
            _mouse?.Unacquire();
            _mouse?.Dispose();
            _mouse = null;

            _keyBoard?.Unacquire();
            _keyBoard?.Dispose();
            _keyBoard = null;

            _directInput?.Dispose();
            _directInput = null;
        }

        public bool Update()
        {
            if (!ReadKeyBoard())
                return false;

            if (!ReadMouse())
                return false;

            ProcessInput();

            return true;
        }

        private void ProcessInput()
        {
        }

        private bool ReadKeyBoard()
        {
            var resultCode = ResultCode.Ok;
            _keyBoardState = new KeyboardState();

            try
            {
                _keyBoard.GetCurrentState(ref _keyBoardState);
            }
            catch (SharpDX.SharpDXException ex)
            {
                resultCode = ex.Descriptor;
            }

            if (resultCode == ResultCode.InputLost || resultCode == ResultCode.NotAcquired)
            {
                try
                {
                    _keyBoard.Acquire();
                }
                catch
                {
                }

                return true;
            }

            if (resultCode == ResultCode.Ok)
                return true;

            return false;

        }

        private bool ReadMouse()
        {
            var resultCode = ResultCode.Ok;
            _mouseState = new MouseState();

            try
            {
                _mouse.GetCurrentState(ref _mouseState);
            }
            catch (SharpDX.SharpDXException ex)
            {
                resultCode = ex.Descriptor;
            }

            if (resultCode == ResultCode.InputLost || resultCode == ResultCode.NotAcquired)
            {
                try
                {
                    _mouse.Acquire();
                }
                catch
                {
                }

                return true;
            }

            if (resultCode == ResultCode.Ok)
                return true;

            return false;
        }

        public static bool GetKeyDown(Key key) => Instance._keyBoardState != null && Instance._keyBoardState.PressedKeys.Contains(key);
    }
}
