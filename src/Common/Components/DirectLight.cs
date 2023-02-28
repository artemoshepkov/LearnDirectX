using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LearnDirectX.src.Common.Components
{
    public class DirectLight : Component
    {
        public Vector4 Color;
        public Vector3 Direction;

        public DirectLight() { }
    }
}
