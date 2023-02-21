using LearnDirectX.src.Common.EngineSystem.Rendering;
using SharpDX.Windows;
using System;

namespace LearnDirectX.src.Common.EngineSystem
{
    public sealed class Engine
    {
        #region Fields

        private static Engine _instance;

        private RenderLayersSet _layersSet;

        #endregion

        #region Properties

        #endregion

        #region Constructor

        private Engine() { }

        #endregion

        #region Public methods
        
        public static Engine GetInstance()
        {
            if (_instance == null )
                _instance = new Engine();
            return _instance;
        }

        public static void Init(string title, int width, int height)
        {
            Window.Init(title, width, height);
            GetInstance()._layersSet = new RenderLayersSet();
        }

        public static void Run()
        {
            RenderLoop.Run(
                Window.Instance.RenderForm,
                () =>
                {
                    Window.UpdateWindow();
                    Window.Clear();


                    if (Input.GetKeyDown(SharpDX.DirectInput.Key.Escape))
                        Console.WriteLine("2");

                    GetInstance()._layersSet.Render();

                    Input.Instance.Update();
                    Window.OnUpdate();
                });
        }

        public void AddRenderLayer(RenderLayer layer) => GetInstance()._layersSet.Add(layer);

        #endregion
    }
}
