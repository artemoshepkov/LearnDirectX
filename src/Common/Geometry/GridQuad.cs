using System.Numerics;

namespace LearnDirectX.src.Common.Geometry
{
    public sealed class GridQuad : Quad
    {
        public readonly Vector3[] TopCorners;
        public readonly Vector3[] BottomCorners;

        public bool IsActive { get; set; }

        public GridQuad(int i, int j, int k, bool active, Vector3[] topCorners, Vector3[] bottomCorners) : base(i, j, k)
        {
            IsActive = active;
            TopCorners = topCorners;
            BottomCorners = bottomCorners;
        }

        public override string ToString()
        {
            return $"{I} {J} {K}";
        }
    }
}
