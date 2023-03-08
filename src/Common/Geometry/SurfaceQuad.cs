using System.Collections.Generic;
using System.Numerics;

namespace LearnDirectX.src.Common.Geometry
{
    public class SurfaceQuad : Quad
    {
        public List<Vector3> Points;

        public SurfaceQuad(int i, int j, int k, List<Vector3> points) : base(i, j, k)
        {
            Points = points;
        }

        public override string ToString()
        {
            string points = "";
            foreach (var point in Points)
            {
                points += point.ToString();
            }
            return $"{I} {J} {K}: {points};";
        }
    }
}
