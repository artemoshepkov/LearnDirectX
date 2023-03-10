using LearnDirectX.src.Common.Components;
using LearnDirectX.src.Common.EngineSystem.Shaders;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace LearnDirectX.src.Common
{
    public class CubeMeshGenerator
    {
        public static Mesh GenerateMesh(float cubeSize, int amountSquareForSide)
        {
            var vertexes = GenerateVertexes(amountSquareForSide, cubeSize);
            var indexes = GenerateIndexes(amountSquareForSide);

            return new Mesh(vertexes, indexes, SharpDX.Direct3D.PrimitiveTopology.TriangleList);
        }
        public static Vertex[] GenerateVertexes(int amountSquareForSide, float cubeSize)
        {
            var oneSquareSize = cubeSize / amountSquareForSide;

            var vertices = new List<Vertex>();

            for (float z = -cubeSize / 2; z <= cubeSize / 2; z += cubeSize)
            {
                for (float y = -cubeSize / 2; y <= cubeSize / 2; y += oneSquareSize)
                {
                    for (float x = -cubeSize / 2; x <= cubeSize / 2; x += oneSquareSize)
                    {
                        vertices.Add(
                            new Vertex()
                            {
                                Position = new Vector3(x, y, z),
                                Normal = new Vector3(0, 0, z),
                            });
                    }
                }
            }

            for (float y = -cubeSize / 2; y <= cubeSize / 2; y += cubeSize)
            {
                for (float z = -cubeSize / 2; z <= cubeSize / 2; z += oneSquareSize)
                {
                    for (float x = -cubeSize / 2; x <= cubeSize / 2; x += oneSquareSize)
                    {
                        vertices.Add(
                            new Vertex()
                            {
                                Position = new Vector3(x, y, z),
                                Normal = new Vector3(0, y, 0),
                            });
                    }
                }
            }

            for (float x = -cubeSize / 2; x <= cubeSize / 2; x += cubeSize)
            {
                for (float z = -cubeSize / 2; z <= cubeSize / 2; z += oneSquareSize)
                {
                    for (float y = -cubeSize / 2; y <= cubeSize / 2; y += oneSquareSize)
                    {
                        vertices.Add(
                            new Vertex()
                            {
                                Position = new Vector3(x, y, z),
                                Normal = new Vector3(x, 0, 0),
                            });
                    }
                }
            }

            return vertices.ToArray();
        }

        public static ushort[] GenerateIndexes(int amountSquareForSide)
        {
            var indexes = new List<uint>();

            var amountPassVertices = Math.Pow(amountSquareForSide + 1, 2);

            for (uint k = 0; k < 6; k++)
            {
                for (uint i = 0; i < amountSquareForSide; i++)
                {
                    uint coef = i * (uint)(amountSquareForSide + 1) + k * (uint)amountPassVertices;
                    for (uint j = 0; j < amountSquareForSide; j++)
                    {
                        indexes.Add(j + coef);
                        indexes.Add(j + 1 + coef);
                        indexes.Add(j + (uint)amountSquareForSide + 1 + coef);

                        indexes.Add(j + 1 + coef);
                        indexes.Add(j + (uint)amountSquareForSide + 1 + coef);
                        indexes.Add(j + (uint)amountSquareForSide + 2 + coef);
                    }
                }
            }

            List<ushort> res = new List<ushort>();

            foreach (var n in indexes)
            {
                res.Add((ushort)n);
            }

            return res.ToArray();
        }
    }
}
