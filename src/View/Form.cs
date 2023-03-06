using DevExpress.XtraEditors;
using LearnDirectX.src.Common.Components;
using LearnDirectX.src.Common.EngineSystem;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LearnDirectX
{
    public partial class Form : XtraForm
    {
        private Scene _selectedScene;

        private GameObject _selectedGameObject;

        public bool IsCursorHide { get; set; }

        public Form()
        {
            InitializeComponent();

            Engine.AddEventSceneChanged(SetScene);
        }

        public void HideCursor(bool isHide)
        {
            IsCursorHide = isHide;

            if (IsCursorHide)
            {
                Cursor.Hide();
                Cursor.Clip = Bounds;
            }
            else
            {
                Cursor.Show();
                Cursor.Clip = Rectangle.Empty;
            }
        }
            

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void SetScene()
        {
            _selectedScene = Engine.SelectedScene;

            if (_selectedScene.GameObjects.Count == 0)
                return;

            _selectedGameObject = _selectedScene.GameObjects.First();

            foreach (var gObj in _selectedScene.GameObjects)
            {
                GameObjectList.Items.Add(gObj.Name);
            }

            GameObjectList.SelectedIndex = 0;

            LoadTransformSelectedObjectToGUI();
        }

        private void GameObjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedGameObject = _selectedScene.GameObjects[GameObjectList.SelectedIndex];

            LoadTransformSelectedObjectToGUI();
        }

        private void LoadTransformSelectedObjectToGUI()
        {
            var transform = _selectedGameObject.GetComponent<Transform>();

            TransformX.Text = transform.Position.X.ToString();
            TransformY.Text = transform.Position.Y.ToString();
            TransformZ.Text = transform.Position.Z.ToString();

            RotationX.Text = transform.Rotation.X.ToString();
            RotationY.Text = transform.Rotation.Y.ToString();
            RotationZ.Text = transform.Rotation.Z.ToString();

        }


        private void Form_MouseEnter(object sender, EventArgs e)
        {
            if (IsCursorHide)
                Cursor.Clip = Bounds;
        }

        private void TransformX_KeyPress(object sender, KeyPressEventArgs e)
        {
            LimitTextBoxInputOfDigits(sender, e);
        }
        private void TransformY_KeyPress(object sender, KeyPressEventArgs e)
        {
            LimitTextBoxInputOfDigits(sender, e);
        }
        private void TransformZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            LimitTextBoxInputOfDigits(sender, e);
        }
        private void LimitTextBoxInputOfDigits(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                e.Handled = true;
        }
       
        private void TransformX_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            var transform = _selectedGameObject.GetComponent<Transform>();

            transform.Translate(new System.Numerics.Vector3(float.Parse(textBox.Text), transform.Position.Y, transform.Position.Z));
        }
        private void TransformY_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            var transform = _selectedGameObject.GetComponent<Transform>();

            transform.Translate(new System.Numerics.Vector3(transform.Position.X, float.Parse(textBox.Text), transform.Position.Z));
        }
        private void TransformZ_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            var transform = _selectedGameObject.GetComponent<Transform>();

            transform.Translate(new System.Numerics.Vector3(transform.Position.X, transform.Position.Y, float.Parse(textBox.Text)));
        }

        private void RotationX_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            var transform = _selectedGameObject.GetComponent<Transform>();

            transform.Rotate(new System.Numerics.Vector3(float.Parse(textBox.Text), transform.Rotation.Y, transform.Rotation.Z));
        }
        private void RotationY_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            var transform = _selectedGameObject.GetComponent<Transform>();

            transform.Rotate(new System.Numerics.Vector3(transform.Rotation.X, float.Parse(textBox.Text), transform.Rotation.Z));
        }
        private void RotationZ_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            var transform = _selectedGameObject.GetComponent<Transform>();

            transform.Rotate(new System.Numerics.Vector3(transform.Rotation.X, transform.Rotation.Y, float.Parse(textBox.Text)));
        }

        private void RotationX_KeyPress(object sender, KeyPressEventArgs e)
        {
            LimitTextBoxInputOfDigits(sender, e);
        }
        private void RotationY_KeyPress(object sender, KeyPressEventArgs e)
        {
            LimitTextBoxInputOfDigits(sender, e);
        }
        private void RotationZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            LimitTextBoxInputOfDigits(sender, e);
        }

    }
}