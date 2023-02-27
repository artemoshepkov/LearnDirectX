using SharpDX.DirectInput;
using System;
using System.Numerics;

namespace LearnDirectX.src.Common.EngineSystem.Input
{
    public sealed class MouseInput : BaseInput
    {
        private static MouseInput _instance;

        private Mouse _mouse;
        private MouseState _mouseState;

        public static Vector3 MousePos => 
            Instance._mouseState != null 
            ? new Vector3(Instance._mouseState.X, Instance._mouseState.Y, Instance._mouseState.Z) 
            : new Vector3();

        private MouseInput() { }

        public static MouseInput Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MouseInput();
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
            _mouse = new Mouse(_directInput);
            _mouse.Properties.AxisMode = DeviceAxisMode.Relative;
            _mouse.SetCooperativeLevel(Window.Instance.RenderForm.Handle, CooperativeLevel.Foreground | CooperativeLevel.NonExclusive);

            try
            {

                _mouse.Acquire();
            }
            catch
            {
            }

            Engine.AddEventUpdate(Update); // To another class
        }

        public new void Dispose()
        {
            base.Dispose();

            _mouse?.Unacquire();
            _mouse?.Dispose();
            _mouse = null;
        }

        public override void Update()
        {
            ReadMouse();
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

    }
}
