using DevExpress.Utils.Extensions;
using DevExpress.Utils.Layout;
using LearnDirectX.src.Common.Components.GridTask;
using System;
using System.Numerics;
using System.Windows.Forms;

namespace LearnDirectX.src.Common.Components
{
    public class SliceRenderer : Component
    {
        private bool _sliceI;
        private bool _sliceJ;
        private bool _sliceK;

        private int _i;
        private int _j;
        private int _k;

        public bool IsSliceI
        {
            get
            {
                return _sliceI;
            }
            set
            {
                _sliceI = value;
                UpdateSlices();
            }
        }
        public bool IsSliceJ
        {
            get
            {
                return _sliceJ;
            }
            set
            {
                _sliceJ = value;
                UpdateSlices();
            }
        }
        public bool IsSliceK
        {
            get
            {
                return _sliceK;
            }
            set
            {
                _sliceK = value;
                UpdateSlices();
            }
        }

        public int I
        {
            get
            {
                return _i;
            }
            set
            {
                _i = value;
                if (IsSliceI)
                    UpdateSlices();
            }
        }
        public int J
        {
            get
            {
                return _j;
            }
            set
            {
                _j = value;
                if (IsSliceJ)
                    UpdateSlices();
            }
        }
        public int K
        {
            get
            {
                return _k;
            }
            set
            {
                _k = value;
                if (IsSliceK)
                    UpdateSlices();
            }
        }

        public SliceRenderer()
        {
            _i = _j = _k = 0;
            _sliceI = false;
            _sliceJ = false;
            _sliceK = false;
        }

        public void UpdateSlices()
        {
            var quads = Owner.Children;

            if (!(IsSliceI || IsSliceJ || IsSliceK))
            {
                var gridSize = Owner.GetComponent<Grid>().Size;

                foreach (var quad in quads)
                {
                    var quadIndex = quad.GetComponent<QuadIndex>();
                    if (quadIndex.I == 0 || quadIndex.J == 0 || quadIndex.K == 0)
                    {
                        quad.IsEnabled = true;
                    }
                    else if (quadIndex.I == gridSize.X - 1 || quadIndex.J == gridSize.Y - 1 || quadIndex.K == gridSize.Z - 1)
                    {
                        quad.IsEnabled = true;
                    }
                    else
                    {
                        quad.IsEnabled = false;
                    }
                }

                return;
            }

            foreach (var quad in quads)
            {
                var quadIndex = quad.GetComponent<QuadIndex>();
                if (IsSliceI && quadIndex.I == I || IsSliceJ && quadIndex.J == J || IsSliceK && quadIndex.K == K)
                {
                    quad.IsEnabled = true;
                }
                else
                {
                    quad.IsEnabled = false;
                }
            }
        }

        public override GroupBox GetGUI()
        {
            var basePanel = new StackPanel();
            basePanel.Dock = DockStyle.Fill;
            basePanel.LayoutDirection = StackPanelLayoutDirection.TopDown;
            basePanel.AutoSize = true;

            {
                var gridSize = Owner.GetComponent<Grid>().Size;

                #region I slice

                {
                    var layout = new StackPanel();
                    layout.LayoutDirection = StackPanelLayoutDirection.LeftToRight;
                    layout.AutoSize = true;
                    basePanel.AddControl(layout);

                    var checkBox = new CheckBox();
                    checkBox.Text = "I slice";
                    checkBox.Checked = IsSliceI;
                    checkBox.CheckedChanged += (object sender, EventArgs e) =>
                    {
                        var checkBoxI = (CheckBox)sender;

                        IsSliceI = checkBoxI.Checked;
                    };
                    layout.AddControl(checkBox);

                    var trackBar = new TrackBar();
                    trackBar.Value = I;
                    trackBar.Maximum = (int)gridSize.X - 1;
                    trackBar.ValueChanged += (object sender, EventArgs e) =>
                    { 
                        var trackBarI = (TrackBar)sender;

                        I = trackBarI.Value;
                    };
                    layout.AddControl(trackBar);
                }

                #endregion

                #region J slice

                {
                    var layout = new StackPanel();
                    layout.LayoutDirection = StackPanelLayoutDirection.LeftToRight;
                    layout.AutoSize = true;
                    basePanel.AddControl(layout);

                    var checkBox = new CheckBox();
                    checkBox.Text = "J slice";
                    checkBox.Checked = IsSliceJ;
                    checkBox.CheckedChanged += (object sender, EventArgs e) =>
                    {
                        var checkBoxJ = (CheckBox)sender;

                        IsSliceJ = checkBoxJ.Checked;
                    };
                    layout.AddControl(checkBox);

                    var trackBar = new TrackBar();
                    trackBar.Value = J;
                    trackBar.Maximum = (int)gridSize.Y - 1;
                    trackBar.ValueChanged += (object sender, EventArgs e) =>
                    {
                        var trackBarJ = (TrackBar)sender;

                        J = trackBarJ.Value;
                    };
                    layout.AddControl(trackBar);
                }

                #endregion

                #region K slice

                {
                    var layout = new StackPanel();
                    layout.LayoutDirection = StackPanelLayoutDirection.LeftToRight;
                    layout.AutoSize = true;
                    basePanel.AddControl(layout);

                    var checkBox = new CheckBox();
                    checkBox.Text = "K slice";
                    checkBox.Checked = IsSliceK;
                    checkBox.CheckedChanged += (object sender, EventArgs e) =>
                    {
                        var checkBoxK = (CheckBox)sender;

                        IsSliceK = checkBoxK.Checked;
                    };
                    layout.AddControl(checkBox);

                    var trackBar = new TrackBar();
                    trackBar.Value = K;
                    trackBar.Maximum = (int)gridSize.Z - 1;
                    trackBar.ValueChanged += (object sender, EventArgs e) =>
                    {
                        var trackBarJ = (TrackBar)sender;

                        K = trackBarJ.Value;
                    };
                    layout.AddControl(trackBar);
                }

                #endregion
            }

            var groupBox = new GroupBox();
            groupBox.Text = "SliceRenderer";
            groupBox.AutoSize = true;
            groupBox.Dock = DockStyle.Top;
            groupBox.AddControl(basePanel);

            return groupBox;
        }
    }
}
