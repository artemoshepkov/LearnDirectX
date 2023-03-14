using DevExpress.Utils.Extensions;
using DevExpress.Utils.Layout;
using DevExpress.XtraBars.Docking.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Design;
using LearnDirectX.src.Common.Components;
using LearnDirectX.src.Common.EngineSystem;
using LearnDirectX.src.View.Controls;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LearnDirectX
{
    public partial class Form : XtraForm
    {
        private GameObject _selectedGameObject;

        public bool IsCursorHide { get; set; }

        public Form()
        {
            InitializeComponent();

            Engine.AddEventSceneChanged(SetScene);
            Engine.AddEventScenesListChanged(UpdateScenesList);
        }

        private void UpdateScenesList()
        {
            if (Engine.Scenes.Count == 0)
                return;

            ListBoxScenes.Items.Clear();
            foreach (var scene in Engine.Scenes)
            {
                ListBoxScenes.Items.Add(scene);
            }
        }

        private void SetScene()
        {
            ListBoxScenes.SelectedItem = Engine.SelectedScene;

            if (Engine.SelectedScene.GameObjects.Count == 0)
                return;

            _selectedGameObject = Engine.SelectedScene.GameObjects.First();
            SetGameObjects();
        }

        private void SetGameObjects()
        {

            TreeViewGameObjects.Nodes.Clear();
            foreach (var gObj in Engine.SelectedScene.GameObjects)
            {
                TreeViewGameObjects.Nodes.Add(SetNodeGameObject(gObj));
            }

            foreach (var gObj in Engine.SelectedScene.Lights)
            {
                TreeViewGameObjects.Nodes.Add(SetNodeGameObject(gObj));
            }

            if (TreeViewGameObjects.Nodes.Count == 0)
                return;

            LoadGameObjectInspector();
        }

        private TreeNode SetNodeGameObject(GameObject gObj)
        {
            var parentNode = new TreeNodeGameObject(gObj);

            if (gObj.Children != null)
            {
                foreach (var child in gObj.Children)
                {
                    parentNode.Nodes.Add(SetNodeGameObject(child));
                }
            }

            return parentNode;
        }

        private void LoadGameObjectInspector()
        {
            Inspector.Controls.Clear();
            foreach (var component in _selectedGameObject.ComponentManager.Components.Values)
            {
                var gui = component.GetGUI();
                if (gui != null)
                    Inspector.AddControl(gui);
            }
        }

        private void TreeViewGameObjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var selectedNode = TreeViewGameObjects.SelectedNode as TreeNodeGameObject;

            _selectedGameObject = selectedNode.GameObjectItem;

            LoadGameObjectInspector();
        }

        private void ListBoxScenes_SelectedValueChanged(object sender, EventArgs e)
        {
            var selectedScene = ListBoxScenes.SelectedItem;

            Engine.SelectedScene = selectedScene as Scene;

            ActiveControl = null;
        }

        private void Form_MouseEnter(object sender, EventArgs e)
        {
            if (IsCursorHide)
                Cursor.Clip = Bounds;
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
    }
}