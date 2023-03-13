using System;
using System.Linq;
using System.Numerics;

namespace LearnDirectX.src.Common.Components.GridTask
{
    public class MaterialUpdater : Component
    {
        public MaterialUpdater() { }

        public override void Start()
        {
            var prop = Owner.GetComponent<FloatProperty>();
            prop.AddEventValueChanged(UpdateMaterial);
            UpdateMaterial();
        }

        public void UpdateMaterial()
        {
            var material = Owner.GetComponent<Material>();

            var prop = Owner.GetComponent<FloatProperty>();

            var palette = Owner.Parent.GetComponent<Palette>();

            var absolutePosOnPalette = (prop.Value - prop.MinValue) / prop.MaxValue;

            var colorStep = (1f / (float)(palette.Colors.Count - 1));

            Tuple<int, Vector4> leftColor = new Tuple<int, Vector4>(0, palette.Colors.First()); 
            Tuple<int, Vector4> rightColor = new Tuple<int, Vector4>(0, palette.Colors.First());
            for (int i = 1; i < palette.Colors.Count; i++)
            {
                if (absolutePosOnPalette < i * colorStep)
                {
                    leftColor = new Tuple<int, Vector4>(i - 1, palette.Colors[i - 1]);
                    rightColor = new Tuple<int, Vector4>(i, palette.Colors[i]);
                    break;
                }
            }

            var relativePosOnPalette = (absolutePosOnPalette - leftColor.Item1 * colorStep) / (rightColor.Item1 * colorStep - leftColor.Item1 * colorStep);

            var leftR = leftColor.Item2.X;
            var leftG = leftColor.Item2.Y;
            var leftB = leftColor.Item2.Z;

            var rightR = rightColor.Item2.X;
            var rightG = rightColor.Item2.Y;
            var rightB = rightColor.Item2.Z;

            // Find general equation for two points (red of left and right color) and substitute y (relativePos)

            float redComp = (rightR - leftR) == 0 ? 0 : (relativePosOnPalette - leftR) / (rightR - leftR);
            var greenComp = (rightG - leftG) == 0 ? 0 : (relativePosOnPalette - leftG) / (rightG - leftG);
            var blueComp = (rightB - leftB) == 0 ? 0 : (relativePosOnPalette - leftB) / (rightB - leftB);

            material.Color = new Vector4(redComp, greenComp, blueComp, 1f);
        }
    }
}
