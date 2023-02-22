using SharpDX.DirectWrite;
using System;
using System.Data;
using System.Numerics;

namespace LearnDirectX.src.Common.Components
{
    public class Camera : Component
    {
        public float Yaw { get; set; } = -90f;
        public float Pitch { get; set; } = 0f;
        public Vector3 Front { get; private set; }
        public Vector3 Right { get; private set; }
        public Vector3 Up { get; private set; }
        public float AspectRatio { get; set; } = 1f;

        public float Fov { get; private set; } = 60f;
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
            front.X = (float)(Math.Cos(ConvertToRadians(Yaw)) * Math.Cos(ConvertToRadians(Pitch)));
            front.Y = (float)Math.Sin(ConvertToRadians(Pitch));
            front.Z = (float)(Math.Sin(ConvertToRadians(Yaw)) * Math.Cos(ConvertToRadians(Pitch)));

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
            return Matrix4x4.CreatePerspectiveFieldOfView((float)ConvertToRadians(Fov), AspectRatio, 0.01f, 1000f); ;
        }

        private double ConvertToRadians(float angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
