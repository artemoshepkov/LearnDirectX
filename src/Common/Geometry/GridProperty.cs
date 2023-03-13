using System.Collections.Generic;

namespace LearnDirectX.src.Common.Geometry
{
    public sealed class GridProperty
    {
        public string Name { get; set; }

        public List<QuadProperty> QuadProperties { get; set; }

        public float MinValue { get; set; }
        public float MaxValue { get; set; }

        public GridProperty(string name)
        {
            Name = name;
            QuadProperties = new List<QuadProperty>();
            MinValue = float.MaxValue;
            MaxValue = float.MinValue;
        }
    }
}
