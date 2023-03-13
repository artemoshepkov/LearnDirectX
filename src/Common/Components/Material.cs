using DevExpress.Utils.Extensions;
using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;
using System;
using System.Drawing;
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
            basePanel.LayoutDirection = StackPanelLayoutDirection.TopDown;

            {
                var layout = new StackPanel();
                layout.LayoutDirection = StackPanelLayoutDirection.LeftToRight;
                basePanel.AddControl(layout);

                var label = new Label();
                label.Text = "Color";
                layout.AddControl(label);

                var colorPicker = new ColorPickEdit();
                layout.AddControl(colorPicker);
            }

            var groupBox = new GroupBox();
            groupBox.Text = "Material";
            groupBox.Height = 200;
            groupBox.Dock = DockStyle.Top;
            groupBox.AddControl(basePanel);

            return groupBox;
        }
    }
}
