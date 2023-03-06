using System.Collections.Generic;
using System.Numerics;

namespace LearnDirectX.src.Common.Entity
{
    public class Quad
    {
        public int I;
        public int J;
        public int K;

        public List<Vector3> Points;

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
