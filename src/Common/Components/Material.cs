using DevExpress.Utils.Extensions;
using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;
using System;
using System.Numerics;
using System.Windows.Forms;

namespace LearnDirectX.src.Common.Components
{
    public class Material : Component
    {
        public Vector4 Color;

        public Material()
        {

        }

        public Material(Vector4 color)
        {
            Color = color;
        }

        public override GroupBox GetGUI()
        {
            var basePanel = new StackPanel();
            basePanel.Dock = DockStyle.Fill;
            basePanel.AutoSize = true;
            basePanel.LayoutDirection = StackPanelLayoutDirection.TopDown;

            {
                var layout = new StackPanel();
                layout.LayoutDirection = StackPanelLayoutDirection.LeftToRight;
                layout.AutoSize = true;
                basePanel.AddControl(layout);

                var label = new Label();
                label.Text = "Color";
                layout.AddControl(label);

                var colorPicker = new ColorPickEdit();
                colorPicker.Color = System.Drawing.Color.FromArgb((int)Color.W * 255, (int)Color.X * 255, (int)Color.Y * 255, (int)Color.Z * 255);
                colorPicker.ColorChanged += (object sender, EventArgs e) =>
                {
                    var r = colorPicker.Color.R / 255f;
                    var g = colorPicker.Color.G / 255f;
                    var b = colorPicker.Color.B / 255f;
                    var a = colorPicker.Color.A / 255f;

                    Color = new Vector4(r, g, b, a);
                };
                layout.AddControl(colorPicker);
            }

            var groupBox = new GroupBox();
            groupBox.Text = "Material";
            groupBox.AutoSize = true;
            groupBox.Dock = DockStyle.Top;
            groupBox.AddControl(basePanel);

            return groupBox;
        }
    }
}
