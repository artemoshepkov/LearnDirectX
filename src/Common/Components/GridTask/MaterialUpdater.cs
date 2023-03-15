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

            if (prop.MaxValue == prop.MinValue)
            {
                var color = palette.Colors.First().Color;
                material.Color = new Vector4(color.X, color.Y, color.Z, 1f);
                return;
            }

            var absolutePosOnPalette = (prop.Value - prop.MinValue) / (prop.MaxValue - prop.MinValue);

            var leftBound = palette.Colors.First().ValueOnPalette;
            var rightBound = palette.Colors.Last().ValueOnPalette;

            if (absolutePosOnPalette <= leftBound)
            {
                var color = palette.Colors.First().Color;
                material.Color = new Vector4(color.X, color.Y, color.Z, 1f);
                return;
            }
            else if (absolutePosOnPalette >= rightBound)
            {
                var color = palette.Colors.Last().Color;
                material.Color = new Vector4(color.X, color.Y, color.Z, 1f);
                return;
            }

            var leftColor = new PaletteColor(palette.Colors[0].ValueOnPalette, palette.Colors[0].Color);
            var rightColor = new PaletteColor(palette.Colors[1].ValueOnPalette, palette.Colors[1].Color);
            for (int i = 1; i < palette.Colors.Count; i++)
            {
                if (absolutePosOnPalette < palette.Colors[i].ValueOnPalette)
                {
                    leftColor = new PaletteColor(palette.Colors[i - 1].ValueOnPalette, palette.Colors[i - 1].Color);
                    rightColor = new PaletteColor(palette.Colors[i].ValueOnPalette, palette.Colors[i].Color);
                    break;
                }
            }

            var relativePos = (absolutePosOnPalette - leftColor.ValueOnPalette) / (rightColor.ValueOnPalette - leftColor.ValueOnPalette);

            var leftR = leftColor.Color.X;
            var leftG = leftColor.Color.Y;
            var leftB = leftColor.Color.Z;

            var rightR = rightColor.Color.X;
            var rightG = rightColor.Color.Y;
            var rightB = rightColor.Color.Z;

            float redComp = (rightR == leftR) ? rightR : Math.Abs(relativePos - leftR) / Math.Abs(rightR - leftR);
            var greenComp = (rightG == leftG) ? rightG : Math.Abs(relativePos - leftG) / Math.Abs(rightG - leftG);
            var blueComp = (rightB == leftB) ? rightB : Math.Abs(relativePos - leftB) / Math.Abs(rightB - leftB);

            material.Color = new Vector4(redComp, greenComp, blueComp, 1f);
        }
    }
}
