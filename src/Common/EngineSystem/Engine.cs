using LearnDirectX.src.Common.Components;
using LearnDirectX.src.Common.EngineSystem.Rendering;
using SharpDX.DirectInput;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace LearnDirectX.src.Common.EngineSystem
{
    public delegate void UpdateEvent();

    public sealed class Engine
    {
        #region Fields

        private static Engine _instance;

        private RenderLayersSet _layersSet;
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

        #endregion

        #region Constructor

        private Engine() { }

        #endregion

        #region Public methods

        public static void Init(string title, int width, int height)
        {
            Window.Init(title, width, height);
            Instance._layersSet = new RenderLayersSet();
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

                    Instance._layersSet.Render();

                    Update();

                    Profiler.EndFrame();
                });
        }

        public static void AddRenderLayer(RenderLayer layer) => Instance._layersSet.Add(layer);

        public static void AddEventUpdate(UpdateEvent updateEvent) => Instance._updateEvent += updateEvent;

        #endregion

        #region Private methods

        private static void Update() => Instance._updateEvent?.Invoke();

        #endregion
    }
}
