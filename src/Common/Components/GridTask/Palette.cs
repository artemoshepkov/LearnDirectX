using DevExpress.Utils.Extensions;
using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace LearnDirectX.src.Common.Components.GridTask
{
    public class PaletteColor
    {
        public float ValueOnPalette;
        public Vector4 Color;

        public PaletteColor(float value, Vector4 color)
        {
            ValueOnPalette = value;
            Color = color;
        }
    }
    public class Palette : Component
    {
        public List<PaletteColor> Colors;

        public Palette() { }

        public Palette(List<PaletteColor> colors) 
        {
            Colors = colors;
        }

        public override GroupBox GetGUI()
        {
            var basePanel = new StackPanel();
            basePanel.Dock = DockStyle.Fill;
            basePanel.AutoSize = true;
            basePanel.LayoutDirection = StackPanelLayoutDirection.TopDown;

            {
                for (int i = 0; i < Colors.Count; i++)
                {
                    var colorPicker = new ColorPickEdit();
                    colorPicker.Color = System.Drawing.Color.FromArgb((int)Colors[i].Color.W * 255, (int)Colors[i].Color.X * 255, (int)Colors[i].Color.Y * 255, (int)Colors[i].Color.Z * 255);
                    var index = i;
                    colorPicker.ColorChanged += (object sender, EventArgs e) =>
                    {
                        var r = colorPicker.Color.R / 255f;
                        var g = colorPicker.Color.G / 255f;
                        var b = colorPicker.Color.B / 255f;
                        var a = colorPicker.Color.A / 255f;
                        
                        Colors[index].Color = new Vector4(r, g, b, a);

                        Owner.GetComponent<GridProperties>().UpdateProps();
                    };
                    basePanel.Controls.Add(colorPicker);
                }
            }

            var groupBox = new GroupBox();
            groupBox.Text = "Palette";
            groupBox.AutoSize = true;
            groupBox.Dock = DockStyle.Top;
            groupBox.AddControl(basePanel);

            return groupBox;
        }
    }
}
