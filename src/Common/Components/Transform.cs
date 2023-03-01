using System.Numerics;

namespace LearnDirectX.src.Common.Components
{
    public sealed class Transform : Component
    {
        public Matrix4x4 Model { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }


        public Transform() { }

        public Transform(Vector3 position)
        {
            Position = position;
            Model = Matrix4x4.Identity * Matrix4x4.CreateTranslation(Position);
        }
        public Transform(Vector3 position, Vector3 rotation)
        {
            Position = position;
            Rotation = rotation;
            Model = Matrix4x4.Identity * Matrix4x4.CreateTranslation(Position) * Matrix4x4.CreateFromYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z);
        }

        public void Translate(Vector3 translation)
        {
            Position = translation;

            Model = Matrix4x4.CreateTranslation(Position) * Matrix4x4.CreateFromYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z);
        }

        public void Rotate(Vector3 rotation)
        {
            Rotation = rotation;

            Model = Matrix4x4.CreateTranslation(Position) * Matrix4x4.CreateFromYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z);
        }
    }
}
