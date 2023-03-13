using System.Collections.Generic;
using System.Numerics;

namespace LearnDirectX.src.Common.Components.GridTask
{
    public class Palette : Component
    {
        public List<Vector4> Colors;

        public Palette() { }

        public Palette(List<Vector4> colors) 
        {
            Colors = colors;
        }
    }
}
