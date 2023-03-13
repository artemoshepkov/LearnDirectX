using DevExpress.Utils.Drawing;
using DevExpress.Utils.MVVM.Internal;
using DevExpress.XtraEditors.Design;
using LearnDirectX.src.Common.Components.GridTask;
using LearnDirectX.src.Common.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace LearnDirectX.src.Common.Geometry
{
    public class GridLoader
    {
        public static Grid ReadGridFromFile(string path)
        {
            var grid = new Grid();

            using (var binReader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                grid.Size.X = binReader.ReadInt32();
                grid.Size.Y = binReader.ReadInt32();
                grid.Size.Z = binReader.ReadInt32();

                for (int k = (int)grid.Size.Z - 1; k >= 0; k--)
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

        public static List<GridProperty> ReadPropsFromFile(Vector3 gridSize, string path)
        {
            var gridProps = new List<GridProperty>();

            using (StreamReader reader = new StreamReader(path))
            {
                string line;

                line = reader.ReadLine();

                int propsAmount;

                if (!int.TryParse(line, out propsAmount))
                    return null;

                for (int props = 0; props < propsAmount; props++)
                {
                    var propName = reader.ReadLine();

                    gridProps.Add(new GridProperty(propName));

                    for (int k = 0; k < gridSize.Z; k++)
                    {
                        for (int i = 0; i < gridSize.X; i++)
                        {
                            for (int j = 0; j < gridSize.Y; j++)
                            {
                                if ((line = reader.ReadLine()) == null || !float.TryParse(line, out float res))
                                {
                                    throw new Exception("Grid size doesn`t correspond prop file");
                                }

                                if (res > gridProps[props].MaxValue)
                                {
                                    gridProps[props].MaxValue = res;
                                }

                                if (res < gridProps[props].MinValue)
                                {
                                    gridProps[props].MinValue = res;
                                }

                                gridProps[props].QuadProperties.Add(new QuadProperty(new Vector3(i, j, k), res));
                            }
                        }
                    }
                }
            }

            return gridProps;
        }
    }
}
