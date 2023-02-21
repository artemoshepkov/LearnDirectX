using System.Numerics;

namespace LearnDirectX.src.Common.Components
{
    public sealed class Transform : Component
    {
        public Matrix4x4 Model { get; set; }
        public Vector3 Position { get; set; }

        public Transform() { }

        public Transform(Vector3 position)
        {
            Position= position;
            Model = Matrix4x4.Identity;
        }

        public void Translate(Vector3 translation)
        {
            Position += translation;

            var matrix = Matrix4x4.Identity;
            matrix = Matrix4x4.CreateTranslation(translation);
            Model *= matrix;
        }
    }
}
