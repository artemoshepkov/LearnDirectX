using DevExpress.XtraBars.Docking.Helpers;
using DevExpress.XtraEditors;
using LearnDirectX.src.Common.Components;
using LearnDirectX.src.Common.Components.GridTask;
using LearnDirectX.src.Common.EngineSystem;
using SharpDX.Direct2D1.Effects;
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

        private const string _defaultFloatPropString = "Float property - ";

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

            LoadGridOptions();
        }

        private void LoadGridOptions()
        {
            if (_selectedGameObject.GetComponent<Grid>() == null)
            {
                CheckBoxAllGrid.Hide();
                CheckBoxSliceI.Hide();
                CheckBoxSliceJ.Hide();
                CheckBoxSliceK.Hide();
                TrackBarSliceI.Hide();
                TrackBarSliceJ.Hide();
                TrackBarSliceK.Hide();
                return;
            }
            
            LoadSlicesDataToGUI();
        }

        private void LoadSlicesDataToGUI()
        {
            var gridSize = _selectedGameObject.GetComponent<Grid>().Size;

            TrackBarSliceI.Maximum = (int)gridSize.X - 1;
            TrackBarSliceJ.Maximum = (int)gridSize.Y - 1;
            TrackBarSliceK.Maximum = (int)gridSize.Z - 1;

            var sliceRenderer = _selectedGameObject.GetComponent<SliceRenderer>();

            TrackBarSliceI.Value = sliceRenderer.I;
            TrackBarSliceJ.Value = sliceRenderer.J;
            TrackBarSliceK.Value = sliceRenderer.K;

            CheckBoxSliceI.Checked = sliceRenderer.SliceI;
            CheckBoxSliceJ.Checked = sliceRenderer.SliceJ;
            CheckBoxSliceK.Checked = sliceRenderer.SliceK;
        }

        private void GameObjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedGameObject = _selectedScene.GameObjects[GameObjectList.SelectedIndex];

            LoadTransformSelectedObjectToGUI();

            var prop = _selectedGameObject.GetComponent<FloatProperty>();

            if (prop == null)
            {
                FloatPropLabel.Hide();
                TrackBarFloatProp.Hide();

                return;
            }

            FloatPropLabel.Text = _defaultFloatPropString + prop.Value;
            TrackBarFloatProp.Value = (int)prop.Value;
            TrackBarFloatProp.Minimum = (int)prop.MinValue;
            TrackBarFloatProp.Maximum = (int)prop.MaxValue;
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;
        }
       
        private void TransformX_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            if (textBox.Text == "" || textBox.Text == "-" || textBox.Text == ".")
                return;

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

        private void TrackBarFloatProp_ValueChanged(object sender, EventArgs e)
        {
            var prop = _selectedGameObject.GetComponent<FloatProperty>();
            prop.Value = TrackBarFloatProp.Value;
            FloatPropLabel.Text = _defaultFloatPropString + prop.Value;
        }

        private void CheckBoxSliceI_CheckStateChanged(object sender, EventArgs e)
        {
            if (CheckBoxAllGridVerify())
            {
                CheckBoxSliceI.Checked = false;
                return;
            }

            var sliceRenderer = _selectedGameObject.GetComponent<SliceRenderer>();

            var checkBox = (CheckBox)sender;

            var newState = checkBox.Checked;

            sliceRenderer.SliceI = newState;
        }

        private void CheckBoxSliceJ_CheckStateChanged(object sender, EventArgs e)
        {
            if (CheckBoxAllGridVerify())
            {
                CheckBoxSliceJ.Checked = false;
                return;
            }
            var sliceRenderer = _selectedGameObject.GetComponent<SliceRenderer>();

            var checkBox = (CheckBox)sender;

            var newState = checkBox.Checked;

            sliceRenderer.SliceJ = newState;
        }

        private void CheckBoxSliceK_CheckStateChanged(object sender, EventArgs e)
        {
            if (CheckBoxAllGridVerify())
            {
                CheckBoxSliceK.Checked = false;
                return;
            }

            var sliceRenderer = _selectedGameObject.GetComponent<SliceRenderer>();

            var checkBox = (CheckBox)sender;

            var newState = checkBox.Checked;

            sliceRenderer.SliceK = newState;
        }

        private bool CheckBoxAllGridVerify() => CheckBoxAllGrid.Checked;

        private void CheckBoxAllGrid_CheckStateChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;

            var newState = checkBox.Checked;

            if (newState)
            {
                var sliceRenderer = _selectedGameObject.GetComponent<SliceRenderer>();
                sliceRenderer.SliceI = false;
                CheckBoxSliceI.Checked = false;
                sliceRenderer.SliceJ = false;
                CheckBoxSliceJ.Checked = false;
                sliceRenderer.SliceK = false;
                CheckBoxSliceK.Checked = false;
            }
        }

        private void TrackBarSliceI_ValueChanged(object sender, EventArgs e)
        {
            var sliceRenderer = _selectedGameObject.GetComponent<SliceRenderer>();

            var trackBar = (TrackBar)sender;

            sliceRenderer.I = trackBar.Value;
        }

        private void TrackBarSliceJ_ValueChanged(object sender, EventArgs e)
        {
            var sliceRenderer = _selectedGameObject.GetComponent<SliceRenderer>();

            var trackBar = (TrackBar)sender;

            sliceRenderer.J = trackBar.Value;
        }

        private void TrackBarSliceK_ValueChanged(object sender, EventArgs e)
        {
            var sliceRenderer = _selectedGameObject.GetComponent<SliceRenderer>();

            var trackBar = (TrackBar)sender;

            sliceRenderer.K = trackBar.Value;
        }
    }
}