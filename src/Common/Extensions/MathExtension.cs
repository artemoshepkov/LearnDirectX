using System;

namespace LearnDirectX.src.Common.Extensions
{
    public static class MathExtension
    {
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0)
                return min;
            else if (value.CompareTo(max) > 0)
                return max;

            return value;
        }
        public static float ConvertToRadians(this float angle)
        {
            return (float)(Math.PI / 180) * angle;
        }
    }
}
