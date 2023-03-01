using DevExpress.Data.Linq.Helpers;
using LearnDirectX.src.Common.Extensions;
using SharpDX.DirectWrite;
using System;
using System.Data;
using System.Numerics;

namespace LearnDirectX.src.Common.Components
{
    public class Camera : Component
    {

        private float _fov = 60f;
        private float _pitch = 0f;

        public readonly float MinFov = 30f;
        public readonly float MaxFov = 120f;

        public readonly float MinPitch = -90f;
        public readonly float MaxPitch = 90f;

        public Vector3 Front { get; private set; }
        public Vector3 Right { get; private set; }
        public Vector3 Up { get; private set; }
        public float AspectRatio { get; set; } = 1f;
        public float Yaw { get; set; } = 90f;
        public float Pitch
        {
            get
            {
                return _pitch;
            }
            set
            {
                _pitch = value;
                _pitch = _pitch.Clamp(MinPitch, MaxPitch);
            }
        }
        public float Fov 
        {
            get
            {
                return _fov;
            }
            set
            {
                _fov = value;
                _fov = _fov.Clamp(MinFov, MaxFov);
            }
        }

        public Camera()
        {
        }

        public Camera(Vector3 front, Vector3 right, Vector3 up, float fov)
        {
            Front = front;
            Right = right;
            Up = up;
            Fov = fov;
        }

        public void UpdateVectors()
        {
            Vector3 front = new Vector3();
            front.X = (float)(Math.Cos(Yaw.ConvertToRadians()) * Math.Cos(Pitch.ConvertToRadians()));
            front.Y = (float)Math.Sin(Pitch.ConvertToRadians());
            front.Z = (float)(Math.Sin(Yaw.ConvertToRadians()) * Math.Cos(Pitch.ConvertToRadians()));

            Front = Vector3.Normalize(front);
            Right = Vector3.Normalize(Vector3.Cross(Front, Vector3.UnitY));
            Up = Vector3.Normalize(Vector3.Cross(Right, Front));
        }

        public Matrix4x4 GetViewMatrix()
        {
            var position = Owner.GetComponent<Transform>().Position;
            return Matrix4x4.CreateLookAt(position, position + Front, Up);
        }

        public Matrix4x4 GetProjectionMatrix()
        {
            return Matrix4x4.CreatePerspectiveFieldOfView(Fov.ConvertToRadians(), AspectRatio, 0.01f, 1000f); ;
        }
    }
}
