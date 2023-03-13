using DevExpress.Utils.Extensions;
using DevExpress.Utils.Layout;
using DevExpress.XtraSpellChecker.Parser;
using LearnDirectX.src.Common.Extensions;
using System;
using System.Numerics;
using System.Windows.Forms;

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

        public override GroupBox GetGUI()
        {
            var textBoxWidth = 50;

            Action<object, KeyPressEventArgs> limitTextBotInputOfDigits = (object sender, KeyPressEventArgs e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
                    e.Handled = true;
            };

            var basePanel = new StackPanel();
            basePanel.Dock = DockStyle.Fill;
            basePanel.LayoutDirection = StackPanelLayoutDirection.TopDown;

            {
                var layout = new StackPanel();
                layout.LayoutDirection = StackPanelLayoutDirection.LeftToRight;
                basePanel.AddControl(layout);

                var labelPos = new Label();
                labelPos.Text = "Property";
                labelPos.AutoSize = true;
                layout.Controls.Add(labelPos);

                var textBox = new TextBox();
                textBox.Text = Value.ToString();
                textBox.AutoSize = true;
                textBox.Width = textBoxWidth;
                textBox.KeyPress += (object sender, KeyPressEventArgs e) => limitTextBotInputOfDigits(sender, e);
                textBox.TextChanged += (object sender, EventArgs e) =>
                {
                    var textBoxF = (TextBox)sender;

                    if (textBoxF.Text == "" || textBoxF.Text == "-" || textBoxF.Text == ".")
                        return;

                    Value = float.Parse(textBoxF.Text);
                };
                layout.Controls.Add(textBox);
            }

            var groupBox = new GroupBox();
            groupBox.Text = "FloatProperty";
            groupBox.Height = 600;
            groupBox.Dock = DockStyle.Top;
            groupBox.AddControl(basePanel);

            return groupBox;
        }
    }
}
