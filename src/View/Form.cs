﻿using DevExpress.Utils.Extensions;
using DevExpress.Utils.Layout;
using DevExpress.XtraBars.Docking.Helpers;
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
            GameObjectList.Items.Clear();
            foreach (var gObj in Engine.SelectedScene.GameObjects)
            {
                GameObjectList.Items.Add(gObj);
            }

            foreach (var gObj in Engine.SelectedScene.Lights)
            {
                GameObjectList.Items.Add(gObj);
            }

            GameObjectList.SelectedItem = _selectedGameObject;

            LoadGameObjectInspector();


            //TreeViewScenes.Nodes.Clear();
            //foreach (var gObj in Engine.SelectedScene.GameObjects)
            //{
            //    if (gObj.Children != null)
            //    {
            //        TreeViewScenes.Nodes.Add(SetNodeGameObject(gObj));
            //    }
            //}
        }

        //private TreeNode SetNodeGameObject(GameObject gObj)
        //{
        //    var parentNode = new TreeNode();
        //    foreach (var node in gObj.Children)
        //    {

        //    }
        //}

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

        private void ListBoxScenes_SelectedValueChanged(object sender, EventArgs e)
        {
            var selectedScene = ListBoxScenes.SelectedItem;

            Engine.SelectedScene = selectedScene as Scene;

            ActiveControl = null;
        }

        private void GameObjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedGameObject = GameObjectList.SelectedItem as GameObject;

            LoadGameObjectInspector();
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