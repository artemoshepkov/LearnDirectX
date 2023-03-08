using System.Numerics;

namespace LearnDirectX.src.Common.Components.GridTask
{
    public class Grid : Component
    {
        public Vector3 Size { get; set; }

        public Grid() { }
        public Grid(Vector3 size) 
        {
            Size = size;
        }

    }
}
