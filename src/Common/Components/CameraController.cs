using LearnDirectX.src.Common.EngineSystem;
using LearnDirectX.src.Common.EngineSystem.Input;
using System;
using System.Collections.Generic;
using System.Numerics;
using Key = SharpDX.DirectInput.Key;

namespace LearnDirectX.src.Common.Components
{
    public class CameraController : Component
    {
        private readonly Dictionary<Key, Vector3> _controlBinds = new Dictionary<Key, Vector3>()
        {
            { Key.W, Vector3.UnitZ },
            { Key.S, -Vector3.UnitZ },
            { Key.A, -Vector3.UnitX },
            { Key.D, Vector3.UnitX },
            { Key.LeftShift, -Vector3.UnitY },
            { Key.Space, Vector3.UnitY },
        };

        private float _speed = 0.01f;
        private float mouseSensitivity = 1f;

        public bool IsEnabled = true;

        public CameraController()
        {
            Engine.AddEventUpdate(Update);
        }

        public void Update()
        {
            if (!IsEnabled)
                return;

            var camera = Owner.GetComponent<Camera>();
            var transform = Owner.GetComponent<Transform>();

            if (camera == null || transform == null)
            {
                Console.WriteLine("Transform and camera are required for CameraContoller");
                return;
            }

            foreach (KeyValuePair<Key, Vector3> bind in _controlBinds)
            {
                if (KeyboardInput.GetKeyPressed(bind.Key))
                {
                    transform.Position += (camera.Front * bind.Value.Z + camera.Right * bind.Value.X + camera.Up * bind.Value.Y) * _speed * Profiler.DeltaTime;
                }
            }

            camera.Yaw += MouseInput.MousePos.X * mouseSensitivity * Profiler.DeltaTime;
            camera.Pitch += -MouseInput.MousePos.Y * mouseSensitivity * Profiler.DeltaTime;

            camera.UpdateVectors();
        }
    }
}
