using DevExpress.XtraEditors.Internal;
using LearnDirectX.src.Common.Components;
using SharpDX.Windows;
using System;
using System.Collections.Generic;

namespace LearnDirectX.src.Common.EngineSystem
{
    public delegate void UpdateEvent();

    public sealed class Engine
    {
        private event Action _selectedSceneChanged;
        private event Action _scenesListChanged;

        private event UpdateEvent _updateEvent;

        #region Fields

        private static Engine _instance;

        private List<Scene> _scenes;

        private Scene _selectedScene;

        #endregion

        #region Properties

        public static Engine Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Engine();
                return _instance;
            }
        }

        public static Scene SelectedScene
        {
            get
            {
                return Instance._selectedScene;
            }
            set 
            { 
                Instance._selectedScene = value;
                SwitchCameraOnOff();
                Instance._selectedSceneChanged?.Invoke();
            }
        }

        public static List<Scene> Scenes
        {
            get
            {
                return Instance._scenes;
            }
            private set
            {
                Instance._scenes = value;
                Instance._scenesListChanged?.Invoke();
            }
        }
        #endregion

        #region Constructor

        private Engine() { }

        #endregion

        #region Public methods

        public static void Init(string title, int width, int height)
        {
            Scenes = new List<Scene>();
            Window.Init(title, width, height);
        }

        public static void Run()
        {
            RenderLoop.Run(
                Window.Instance.RenderForm,
                () =>
                {
                    Window.UpdateWindow();
                    Window.Clear();

                    Profiler.StartFrame();

                    Render();

                    Update(); // Window; KeyboardInput; MouseInput; CameraController;

                    Profiler.EndFrame();
                });
        }

        public static void AddScene(Scene scene)
        {
            Scenes.Add(scene);
            Instance._scenesListChanged?.Invoke();
            SelectedScene = scene;
        }

        public static void AddEventUpdate(UpdateEvent updateEvent) => Instance._updateEvent += updateEvent;

        public static void AddEventSceneChanged(Action sceneChangedEvent) =>
            Instance._selectedSceneChanged += sceneChangedEvent;
        public static void AddEventScenesListChanged(Action scenesListChangedEvent) =>
            Instance._scenesListChanged += scenesListChangedEvent;

        public static void SwitchCameraOnOff()
        {
            var cameraContoller = Instance._selectedScene.Camera.GetComponent<CameraController>();

            cameraContoller.IsEnabled = Window.IsCursorHide;
        }

        #endregion

        #region Private methods

        private static void Update() => Instance._updateEvent?.Invoke();

        private static void Render()
        {
            SceneRenderer.Render(Instance._selectedScene);
        }

        #endregion
    }
}
