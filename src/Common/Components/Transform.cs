using DevExpress.Utils.Extensions;
using DevExpress.Utils.Layout;
using LearnDirectX.src.Common.Extensions;
using System;
using System.Numerics;
using System.Windows.Forms;

namespace LearnDirectX.src.Common.Components
{
    public sealed class Transform : Component
    {
        private Matrix4x4 RotationMat;
        private Matrix4x4 TranslationMat;
        private Matrix4x4 ScaleMat;

        public Matrix4x4 Model { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 ScaleV { get; set; }

        public Transform() { }

        public override void Start()
        {
            Position = new Vector3(0);
            Rotation = new Vector3(0);
            ScaleV = new Vector3(1);

            UpdateTranslationMat();
            UpdateRotationMat();
            UpdateScaleMat();

            UpdateModel();
        }

        public override GroupBox GetGUI()
        {
            var textBoxWidth = 50;

            var basePanel = new StackPanel();
            basePanel.Dock = DockStyle.Fill;
            basePanel.LayoutDirection = StackPanelLayoutDirection.TopDown;

            Action<object, KeyPressEventArgs> limitTextBotInputOfDigits = (object sender, KeyPressEventArgs e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
                    e.Handled = true;
            };

            #region Position

            {
                var layout = new StackPanel();
                layout.LayoutDirection = StackPanelLayoutDirection.LeftToRight;
                basePanel.AddControl(layout);

                var labelPos = new Label();
                labelPos.Text = "Position";
                labelPos.AutoSize = true;
                layout.Controls.Add(labelPos);

                #region X controls

                {
                    var labelX = new Label();
                    labelX.Text = "X";
                    labelX.AutoSize = true;
                    layout.Controls.Add(labelX);

                    var textBoxPosX = new TextBox();
                    textBoxPosX.AutoSize = true;
                    textBoxPosX.Width = textBoxWidth;
                    textBoxPosX.KeyPress += (object sender, KeyPressEventArgs e) => limitTextBotInputOfDigits(sender, e);
                    textBoxPosX.TextChanged += (object sender, EventArgs e) =>
                    {
                        var textBox = (TextBox)sender;

                        if (textBox.Text == "" || textBox.Text == "-" || textBox.Text == ".")
                            return;

                        Translate(new Vector3(float.Parse(textBox.Text), Position.Y, Position.Z));
                    };
                    layout.Controls.Add(textBoxPosX);
                }

                #endregion

                #region Y controls

                {
                    var labelY = new Label();
                    labelY.Text = "Y";
                    labelY.AutoSize = true;
                    layout.Controls.Add(labelY);

                    var textBoxPosY = new TextBox();
                    textBoxPosY.AutoSize = true;
                    textBoxPosY.Width = textBoxWidth;
                    textBoxPosY.KeyPress += (object sender, KeyPressEventArgs e) => limitTextBotInputOfDigits(sender, e);
                    textBoxPosY.TextChanged += (object sender, EventArgs e) =>
                    {
                        var textBox = (TextBox)sender;

                        if (textBox.Text == "" || textBox.Text == "-" || textBox.Text == ".")
                            return;

                        Translate(new Vector3(Position.X, float.Parse(textBox.Text), Position.Z));
                    };
                    layout.Controls.Add(textBoxPosY);
                }

                #endregion

                #region Z controls

                {
                    var labelZ = new Label();
                    labelZ.Text = "Z";
                    labelZ.AutoSize = true;
                    layout.Controls.Add(labelZ);

                    var textBoxPosZ = new TextBox();
                    textBoxPosZ.AutoSize = true;
                    textBoxPosZ.Width = textBoxWidth;
                    textBoxPosZ.KeyPress += (object sender, KeyPressEventArgs e) => limitTextBotInputOfDigits(sender, e);
                    textBoxPosZ.TextChanged += (object sender, EventArgs e) =>
                    {
                        var textBox = (TextBox)sender;

                        if (textBox.Text == "" || textBox.Text == "-" || textBox.Text == ".")
                            return;

                        Translate(new Vector3(Position.X, Position.Y, float.Parse(textBox.Text)));
                    };
                    layout.Controls.Add(textBoxPosZ);
                }

                #endregion

            }

            #endregion

            #region Rotation

            {
                var layout = new StackPanel();
                layout.LayoutDirection = StackPanelLayoutDirection.LeftToRight;
                basePanel.AddControl(layout);

                var label = new Label();
                label.Text = "Rotation";
                label.AutoSize = true;
                layout.Controls.Add(label);

                #region X controls

                {
                    var labelX = new Label();
                    labelX.Text = "X";
                    labelX.AutoSize = true;
                    layout.Controls.Add(labelX);

                    var textBoxPosX = new TextBox();
                    textBoxPosX.AutoSize = true;
                    textBoxPosX.Width = textBoxWidth;
                    textBoxPosX.KeyPress += (object sender, KeyPressEventArgs e) => limitTextBotInputOfDigits(sender, e);
                    textBoxPosX.TextChanged += (object sender, EventArgs e) =>
                    {
                        var textBox = (TextBox)sender;

                        if (textBox.Text == "" || textBox.Text == "-" || textBox.Text == ".")
                            return;

                        Rotate(new Vector3(float.Parse(textBox.Text), Rotation.Y, Rotation.Z));
                    };
                    layout.Controls.Add(textBoxPosX);
                }

                #endregion

                #region Y controls

                {
                    var labelY = new Label();
                    labelY.Text = "Y";
                    labelY.AutoSize = true;
                    layout.Controls.Add(labelY);

                    var textBoxPosY = new TextBox();
                    textBoxPosY.AutoSize = true;
                    textBoxPosY.Width = textBoxWidth;
                    textBoxPosY.KeyPress += (object sender, KeyPressEventArgs e) => limitTextBotInputOfDigits(sender, e);
                    textBoxPosY.TextChanged += (object sender, EventArgs e) =>
                    {
                        var textBox = (TextBox)sender;

                        if (textBox.Text == "" || textBox.Text == "-" || textBox.Text == ".")
                            return;

                        Rotate(new Vector3(Rotation.X, float.Parse(textBox.Text), Rotation.Z));
                    };
                    layout.Controls.Add(textBoxPosY);
                }

                #endregion

                #region Z controls

                {
                    var labelZ = new Label();
                    labelZ.Text = "Z";
                    labelZ.AutoSize = true;
                    layout.Controls.Add(labelZ);

                    var textBoxPosZ = new TextBox();
                    textBoxPosZ.AutoSize = true;
                    textBoxPosZ.Width = textBoxWidth;
                    textBoxPosZ.KeyPress += (object sender, KeyPressEventArgs e) => limitTextBotInputOfDigits(sender, e);
                    textBoxPosZ.TextChanged += (object sender, EventArgs e) =>
                    {
                        var textBox = (TextBox)sender;

                        if (textBox.Text == "" || textBox.Text == "-" || textBox.Text == ".")
                            return;

                        Rotate(new Vector3(Rotation.X, Rotation.Y, float.Parse(textBox.Text)));
                    };
                    layout.Controls.Add(textBoxPosZ);
                }

                #endregion

            }

            #endregion

            #region Scale

            {
                var layout = new StackPanel();
                layout.LayoutDirection = StackPanelLayoutDirection.LeftToRight;
                basePanel.AddControl(layout);

                var label = new Label();
                label.Text = "Scale";
                label.AutoSize = true;
                layout.Controls.Add(label);

                #region X controls

                {
                    var labelX = new Label();
                    labelX.Text = "X";
                    labelX.AutoSize = true;
                    layout.Controls.Add(labelX);

                    var textBoxPosX = new TextBox();
                    textBoxPosX.AutoSize = true;
                    textBoxPosX.Width = textBoxWidth;
                    textBoxPosX.KeyPress += (object sender, KeyPressEventArgs e) => limitTextBotInputOfDigits(sender, e);
                    textBoxPosX.TextChanged += (object sender, EventArgs e) =>
                    {
                        var textBox = (TextBox)sender;

                        if (textBox.Text == "" || textBox.Text == "-" || textBox.Text == ".")
                            return;

                        Scale(new Vector3(float.Parse(textBox.Text), ScaleV.Y, ScaleV.Z));
                    };
                    layout.Controls.Add(textBoxPosX);
                }

                #endregion

                #region Y controls

                {
                    var labelY = new Label();
                    labelY.Text = "Y";
                    labelY.AutoSize = true;
                    layout.Controls.Add(labelY);

                    var textBoxPosY = new TextBox();
                    textBoxPosY.AutoSize = true;
                    textBoxPosY.Width = textBoxWidth;
                    textBoxPosY.KeyPress += (object sender, KeyPressEventArgs e) => limitTextBotInputOfDigits(sender, e);
                    textBoxPosY.TextChanged += (object sender, EventArgs e) =>
                    {
                        var textBox = (TextBox)sender;

                        if (textBox.Text == "" || textBox.Text == "-" || textBox.Text == ".")
                            return;

                        Scale(new Vector3(ScaleV.X, float.Parse(textBox.Text), ScaleV.Z));
                    };
                    layout.Controls.Add(textBoxPosY);
                }

                #endregion

                #region Z controls

                {
                    var labelZ = new Label();
                    labelZ.Text = "Z";
                    labelZ.AutoSize = true;
                    layout.Controls.Add(labelZ);

                    var textBoxPosZ = new TextBox();
                    textBoxPosZ.AutoSize = true;
                    textBoxPosZ.Width = textBoxWidth;
                    textBoxPosZ.KeyPress += (object sender, KeyPressEventArgs e) => limitTextBotInputOfDigits(sender, e);
                    textBoxPosZ.TextChanged += (object sender, EventArgs e) =>
                    {
                        var textBox = (TextBox)sender;

                        if (textBox.Text == "" || textBox.Text == "-" || textBox.Text == ".")
                            return;

                        Scale(new Vector3(ScaleV.X, ScaleV.Y, float.Parse(textBox.Text)));
                    };
                    layout.Controls.Add(textBoxPosZ);
                }

                #endregion

            }

            #endregion

            var groupBox = new GroupBox();
            groupBox.Text = "Transform";
            groupBox.Height = 400;
            groupBox.Dock = DockStyle.Top;
            groupBox.AddControl(basePanel);

            return groupBox;
        }

        public void Translate(Vector3 translation)
        {
            Position = translation;

            UpdateTranslationMat();

            UpdateModel();
        }

        public void Rotate(Vector3 rotation)
        {
            Rotation = rotation;

            UpdateRotationMat();

            UpdateModel();
        }

        public void Scale(Vector3 scale)
        {
            ScaleV = scale;

            UpdateScaleMat();

            UpdateModel();
        }

        private void UpdateTranslationMat()
        {
            if (Owner.Parent != null)
            {
                var parentTransform = Owner.Parent.GetComponent<Transform>();
                TranslationMat = parentTransform.TranslationMat * Matrix4x4.CreateTranslation(Position);
            }
            else
            {
                TranslationMat = Matrix4x4.CreateTranslation(Position);
            }

            if (Owner.Children != null)
            {
                foreach (var child in Owner.Children)
                {
                    child.GetComponent<Transform>().UpdateTranslationMat();
                }
            }

            UpdateModel();
        }

        private void UpdateRotationMat()
        {
            if (Owner.Parent != null)
            {
                var parentTransform = Owner.Parent.GetComponent<Transform>();
                RotationMat = parentTransform.RotationMat * Matrix4x4.CreateFromYawPitchRoll(Rotation.Y.ConvertToRadians(), Rotation.X.ConvertToRadians(), Rotation.Z.ConvertToRadians());
            }
            else
            {
                RotationMat = Matrix4x4.CreateFromYawPitchRoll(Rotation.Y.ConvertToRadians(), Rotation.X.ConvertToRadians(), Rotation.Z.ConvertToRadians());
            }

            if (Owner.Children != null)
            {
                foreach (var child in Owner.Children)
                {
                    child.GetComponent<Transform>().UpdateRotationMat();
                }
            }

            UpdateModel();
        }

        private void UpdateScaleMat()
        {
            if (Owner.Parent != null)
            {
                var parentTransform = Owner.Parent.GetComponent<Transform>();
                ScaleMat = parentTransform.ScaleMat * Matrix4x4.CreateScale(ScaleV);
            }
            else
            {
                ScaleMat = Matrix4x4.CreateScale(ScaleV);
            }

            if (Owner.Children != null)
            {
                foreach (var child in Owner.Children)
                {
                    child.GetComponent<Transform>().UpdateScaleMat();
                }
            }

            UpdateModel();
        }

        private void UpdateModel() => Model = Matrix4x4.Multiply(Matrix4x4.Multiply(TranslationMat, RotationMat), ScaleMat);
    }
}
