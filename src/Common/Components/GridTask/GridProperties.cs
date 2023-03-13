using DevExpress.Utils.Extensions;
using DevExpress.Utils.Layout;
using LearnDirectX.src.Common.Geometry;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LearnDirectX.src.Common.Components.GridTask
{
    public sealed class GridProperties : Component
    {
        private List<GridProperty> _gridProperties;

        private GridProperty _selectedProperty;

        public GridProperties() { }

        public GridProperties(List<GridProperty> gridProperties)
        {
            _gridProperties = gridProperties;
            _selectedProperty = _gridProperties.Last();
        }

        public void UpdateProps()
        {
            foreach (var gObj in Owner.Children)
            {
                var prop = gObj.GetComponent<FloatProperty>();
                var indexes = gObj.GetComponent<QuadIndex>();

                foreach (var p in _selectedProperty.QuadProperties)
                {
                    if (p.Indexes.X == indexes.I && p.Indexes.Y == indexes.J && p.Indexes.Z == indexes.K)
                    {
                        prop.MinValue = _selectedProperty.MinValue;
                        prop.MaxValue = _selectedProperty.MaxValue;
                        prop.Value = p.Value;
                        break;
                    }
                }
            }

        }

        public override GroupBox GetGUI()
        {
            var basePanel = new StackPanel();
            basePanel.Dock = DockStyle.Fill;
            basePanel.LayoutDirection = StackPanelLayoutDirection.TopDown;
            basePanel.AutoSize = true;

            var combobox = new ComboBox();
            combobox.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (var prop in _gridProperties)
            {
                combobox.Items.Add(prop);
            }
            combobox.SelectedItem = _selectedProperty;
            combobox.SelectedValueChanged += (s, e) =>
            {
                _selectedProperty = combobox.SelectedItem as GridProperty;
                UpdateProps();
                
            };
            basePanel.AddControl(combobox);

            var groupBox = new GroupBox();
            groupBox.Text = "GridProperties";
            groupBox.AutoSize = true;
            groupBox.Dock = DockStyle.Top;
            groupBox.AddControl(basePanel);

            return groupBox;
        }
    }
}
