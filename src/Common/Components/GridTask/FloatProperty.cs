using LearnDirectX.src.Common.Extensions;
using System;

namespace LearnDirectX.src.Common.Components.GridTask
{
    public class FloatProperty : Component
    {
        private float _value;

        private event Action <float> _valueChanged;

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
                //_value = _value.Clamp(MinValue, MaxValue);
                _valueChanged?.Invoke(_value);
            }
        } 

        public FloatProperty() 
        {
            
        }

        public FloatProperty(float val)
        {
            Value = val;
        }

        public void AddEventValueChanged(Action<float> handler)
        {
            _valueChanged += handler;
        }
    }
}
