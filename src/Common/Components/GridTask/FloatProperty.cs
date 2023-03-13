using LearnDirectX.src.Common.Extensions;
using System;

namespace LearnDirectX.src.Common.Components.GridTask
{
    public class FloatProperty : Component
    {
        private float _value;

        private event Action _valueChanged;

        public readonly float MinValue;
        public readonly float MaxValue;

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
                _valueChanged?.Invoke();
            }
        } 

        public FloatProperty() 
        {
            
        }

        public FloatProperty(float val, float minValue, float maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            Value = val;
        }

        public void AddEventValueChanged(Action handler)
        {
            _valueChanged += handler;
        }
    }
}
