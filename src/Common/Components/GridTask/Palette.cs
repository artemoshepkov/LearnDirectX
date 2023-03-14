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
    public class Palette : Component
    {
        public List<Vector4> Colors;

        public Palette() { }

        public Palette(List<Vector4> colors) 
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
                    colorPicker.Color = System.Drawing.Color.FromArgb((int)Colors[i].W * 255, (int)Colors[i].X * 255, (int)Colors[i].Y * 255, (int)Colors[i].Z * 255);
                    var index = i;
                    colorPicker.ColorChanged += (object sender, EventArgs e) =>
                    {
                        var r = colorPicker.Color.R / 255f;
                        var g = colorPicker.Color.G / 255f;
                        var b = colorPicker.Color.B / 255f;
                        var a = colorPicker.Color.A / 255f;
                        
                        Colors[index] = new Vector4(r, g, b, a);

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
