using LearnDirectX.src.Common.EngineSystem;
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

        private float _speed = 0.1f;
        private float mouseSensitivity = 0.1f;

        public bool IsEnabled = true;

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

            foreach (var key in _controlBinds.Keys)
            {
                if (Input.GetKeyDown(key))
                {
                    //transform.Position;
                }
            }

        }
    }
}
