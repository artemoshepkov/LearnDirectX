using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace LearnDirectX.src.Common.Geometry
{
    public sealed class Grid
    {
        public Vector3 Size = new Vector3();
        public List<GridQuad> Quads = new List<GridQuad>();

        public GridQuad GetQuadForIndex(int i, int j, int k)
        {
            return Quads.Where(q => q.I == i && q.J == j && q.K == k).FirstOrDefault();
        }
    }

}
