using DevExpress.Utils.Drawing;
using DevExpress.Utils.MVVM.Internal;
using LearnDirectX.src.Common.Geometry;
using System.IO;
using System.Numerics;

namespace LearnDirectX.src.Common.Geometry
{
    public class GridLoader
    {
        public static Grid ReadFromFile(string path)
        {
            var grid = new Grid();

            using (var binReader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                grid.Size.X = binReader.ReadInt32();
                grid.Size.Y = binReader.ReadInt32();
                grid.Size.Z = binReader.ReadInt32();

                for (int k = 0; k < grid.Size.Z; k++)
                {
                    for (int i = 0; i < grid.Size.X; i++)
                    {
                        for (int j = 0; j < grid.Size.Y; j++)
                        {
                            bool active = binReader.ReadBoolean();
                            var topCorners = new Vector3[4];
                            var bottomCorners = new Vector3[4];

                            for (int corner = 0; corner < 4; corner++)
                            {
                                topCorners[corner].X = binReader.ReadSingle();
                                topCorners[corner].Y = binReader.ReadSingle();
                                topCorners[corner].Z = binReader.ReadSingle();

                                bottomCorners[corner].X = binReader.ReadSingle();
                                bottomCorners[corner].Y = binReader.ReadSingle();
                                bottomCorners[corner].Z = binReader.ReadSingle();
                            }

                            grid.Quads.Add(new GridQuad(i, j, k, active, bottomCorners, topCorners));
                        }
                    }
                }
            }

            return grid;
        }
    }
}
