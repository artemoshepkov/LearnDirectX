using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace LearnDirectX.src.Common.Geometry
{
    public class SurfaceLoader
    {
        List<SurfaceQuad> corners;
        public List<SurfaceQuad> ReadCornersFromFile(string path)
        {
            ReadCorners(path);

            return corners;
        }

        private void ReadCorners(string path)
        {
            string[] indexes;
            string[] cornersLine;

            using (StreamReader reader = new StreamReader(path))
            {
                string line;

                line = reader.ReadLine();

                indexes = line.Split();

                var countQuadI = int.Parse(indexes[0]);
                var countQuadJ = int.Parse(indexes[1]);
                var countQuadK = 1;

                corners = new List<SurfaceQuad>(countQuadI * countQuadJ * countQuadK);

                while ((line = reader.ReadLine()) != null)
                {

                    cornersLine = line.Split();
                    cornersLine = cornersLine.Select(c => c.Contains(";") ? c.Trim(';') : c).ToArray();

                    corners.Add(CreateNewQuad(cornersLine));
                }
            }
        }

        private SurfaceQuad CreateNewQuad(string[] cornersLine)
        {
            int i = int.Parse(cornersLine[0]);
            int j = int.Parse(cornersLine[1]);
            int k = int.Parse(cornersLine[2]);

            List<Vector3> points = new List<Vector3>();

            for (int l = 1; l < 5; l++)
            {
                points.Add(new Vector3(float.Parse(cornersLine[3 * l]), float.Parse(cornersLine[3 * l + 1]), float.Parse(cornersLine[3 * l + 2])));
            }

            return new SurfaceQuad(i, j, k, points);
        }
    }
}
