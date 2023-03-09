using System.Numerics;

namespace LearnDirectX.src.Common.Geometry
{
    public class QuadProperty
    {
        public Vector3 Indexes;
        public float Value;

        public QuadProperty(Vector3 indexes, float value) 
        {
            Indexes = indexes;
            Value = value;
        }
    }
}
