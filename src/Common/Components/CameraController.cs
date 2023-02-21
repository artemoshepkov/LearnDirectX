using System;
using System.Collections.Generic;

namespace LearnDirectX.src.Common.Components
{
    public class CameraController : Component
    {
        //private static const Dictionary<>

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

            //foreach ()

        }
    }
}
