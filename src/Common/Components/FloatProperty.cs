using LearnDirectX.src.Common.Extensions;
using System;

namespace LearnDirectX.src.Common.Components
{
    public class FloatProperty : Component
    {
        private float _value;

        public event Action <float> ValueChanged;

        public readonly float MinValue = 0f;
        public readonly float MaxValue = 10f;

        public float Value 
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                _value = _value.Clamp(MinValue, MaxValue);
                ValueChanged?.Invoke(_value);
            }
        } 

        public FloatProperty() 
        {
            
        }

        public FloatProperty(float val)
        {
            Value = val;
        }
    }
}
