using LearnDirectX.src.Common.EngineSystem.Input;
using SharpDX.DirectInput;

namespace LearnDirectX.src.Common.EngineSystem
{
    public sealed class KeyboardInput : BaseInput
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

            try
            {
                _keyBoard.Acquire(); // danger place
            }
            catch
            {
            }

            Engine.AddEventUpdate(Update); // To another class
        }

        public override void Update()
        {
            ReadKeyBoard();
        }

        public static bool GetKeyDown(Key key)
        {
            return GetKeyPressed(key);
        }

        public static bool GetKeyPressed(Key key) =>
            Instance._keyBoardState != null && Instance._keyBoardState.PressedKeys.Contains(key);

        public new void Dispose()
        {
            base.Dispose();

            _keyBoard?.Unacquire();
            _keyBoard?.Dispose();
            _keyBoard = null;
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
