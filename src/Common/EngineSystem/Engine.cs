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

        #region Fields

        private static Engine _instance;

        private List<Scene> _scenes;

        private Scene _selectedScene;

        private event UpdateEvent _updateEvent;

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
            private set 
            { 
                Instance._selectedScene = value;
                Instance._selectedSceneChanged?.Invoke();
            }
        }

        #endregion

        #region Constructor

        private Engine() { }

        #endregion

        #region Public methods

        public static void Init(string title, int width, int height)
        {
            Instance._scenes = new List<Scene>();
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

                    Update(); // Window; KeyboardInput; MouseInput

                    Profiler.EndFrame();
                });
        }

        public static void AddScene(Scene scene)
        {
            Instance._scenes.Add(scene);
            SelectedScene = scene;
        }

        public static void AddEventUpdate(UpdateEvent updateEvent) => Instance._updateEvent += updateEvent;

        public static void AddEventSceneChanged(Action sceneChangedEvent) =>
            Instance._selectedSceneChanged += sceneChangedEvent;

        public static void SwitchCameraOnOff()
        {
            var cameraContoller = Instance._selectedScene.Camera.GetComponent<CameraController>();

            cameraContoller.IsEnabled = !cameraContoller.IsEnabled;
        }

        #endregion

        #region Private methods

        private static void Update() => Instance._updateEvent?.Invoke();

        private static void Render()
        {
            try
            {
                SceneRenderer.Render(Instance._selectedScene);
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("You need to add scene to engine");
            }
        }

        #endregion
    }
}
