using LearnDirectX.src.Common.Extensions;
using System.Numerics;

namespace LearnDirectX.src.Common.Components
{
    public sealed class Transform : Component
    {
        private Matrix4x4 RotationMat;
        private Matrix4x4 TranslationMat;
        private Matrix4x4 ScaleMat;

        public Matrix4x4 Model { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 ScaleV { get; set; }

        public Transform() { }

        public Transform(Vector3 position)
        {
            Position = position;
            Model = Matrix4x4.Identity * Matrix4x4.CreateTranslation(Position);
        }
        public Transform(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            ScaleV = scale;

            ScaleMat = Matrix4x4.CreateScale(ScaleV);
            RotationMat = Matrix4x4.CreateFromYawPitchRoll(Rotation.Y.ConvertToRadians(), Rotation.X.ConvertToRadians(), Rotation.Z.ConvertToRadians());
            TranslationMat = Matrix4x4.CreateTranslation(Position);

            UpdateModel();
        }

        public void Translate(Vector3 translation)
        {
            Position = translation;

            TranslationMat = Matrix4x4.CreateTranslation(Position);

            UpdateModel();
        }

        public void Rotate(Vector3 rotation)
        {
            Rotation = rotation;

            RotationMat = Matrix4x4.CreateFromYawPitchRoll(Rotation.Y.ConvertToRadians(), Rotation.X.ConvertToRadians(), Rotation.Z.ConvertToRadians());

            UpdateModel();
        }

        public void Scale(Vector3 scale)
        {
            ScaleV = scale;

            ScaleMat = Matrix4x4.CreateScale(ScaleV);

            UpdateModel();
        }

        private void UpdateModel() => Model = Matrix4x4.Multiply(Matrix4x4.Multiply(TranslationMat, RotationMat), ScaleMat);
    }
}
