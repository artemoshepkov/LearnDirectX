using LearnDirectX.src.Common.Extensions;
using System.Numerics;

namespace LearnDirectX.src.Common.Components
{
    public sealed class Transform : Component
    {
        private Matrix4x4 RotationMat;
        private Matrix4x4 TranslationMat;

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

            TranslationMat = Matrix4x4.CreateTranslation(Position);

            Model = RotationMat * TranslationMat;
        }

        public void Rotate(Vector3 rotation)
        {
            Rotation = rotation;

            RotationMat = Matrix4x4.CreateFromYawPitchRoll(Rotation.Y.ConvertToRadians(), Rotation.X.ConvertToRadians(), Rotation.Z.ConvertToRadians());

            Model = RotationMat * TranslationMat;
        }
    }
}
