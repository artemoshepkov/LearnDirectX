using LearnDirectX.src.Common.EngineSystem.Input;
using SharpDX.DirectInput;
using System;

namespace LearnDirectX.src.Common.EngineSystem
{
    public sealed class KeyboardInput : BaseInput, IDisposable
    {
        private static KeyboardInput _instance;

        private Keyboard _keyBoard;
        private KeyboardState _keyBoardState;

        private KeyboardInput() { }

        public static KeyboardInput Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new KeyboardInput();
                    _instance.Init();
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public override void Init()
        {
            _keyBoard = new Keyboard(_directInput);
            _keyBoard.Properties.BufferSize = 256;
            _keyBoard.SetCooperativeLevel(Window.Instance.RenderForm.Handle, CooperativeLevel.Foreground | CooperativeLevel.NonExclusive);

            _keyBoard.Acquire(); // danger place

            Engine.AddEventUpdate(Update); // To another class
        }

        public override void Update()
        {
            ReadKeyBoard();
        }

        public static bool GetKeyDown(Key key) => Instance._keyBoardState != null && Instance._keyBoardState.PressedKeys.Contains(key);

        public void Dispose()
        {
            _keyBoard?.Unacquire();
            _keyBoard?.Dispose();
            _keyBoard = null;

            _directInput?.Dispose();
            _directInput = null;
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
    }
}
