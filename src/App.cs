using LearnDirectX.src.Common.Components;
using LearnDirectX.src.Common.EngineSystem;
using LearnDirectX.src.Common.EngineSystem.Rendering;
using LearnDirectX.src.Common.EngineSystem.Shaders;
using SharpDX.D3DCompiler;
using System;
using System.Collections.Generic;
using System.Numerics;
using Key = SharpDX.DirectInput.Key;
using VertexShader = LearnDirectX.src.Common.EngineSystem.Shaders.VertexShader;
using PixelShader = LearnDirectX.src.Common.EngineSystem.Shaders.PixelShader;

using SharpDX.Direct3D11;
using System.IO;
using SharpDX.Direct3D;
using System.Runtime.Remoting.Contexts;

namespace LearnDirectX.src
{
    public sealed class App
    {
        private DirectXSceneRenderer _renderer;
        private Dictionary<Key, Action> _controlBinds;

        public List<Scene> Scenes;

        private bool isPoligon = false;

        public App() 
        {
            _renderer = new DirectXSceneRenderer(InitializeScene());

            _controlBinds = new Dictionary<Key, Action>()
            {
                { Key.Escape, Window.Exit },
                { Key.R, Window.ChangeCursorMode },
                { Key.F, SetPoligonMode },
            };

            Engine.AddRenderLayer(_renderer);
            Engine.AddEventUpdate(Update);
        }

        private void SetPoligonMode()
        {
            RasterizerStateDescription rasterDesc;
            var context = Window.Instance.Device.ImmediateContext;

            if (context.Rasterizer.State != null)
                rasterDesc = context.Rasterizer.State.Description;
            else
                rasterDesc = new RasterizerStateDescription()
                {
                    CullMode = CullMode.Back,
                    FillMode = FillMode.Solid
                };

            if (isPoligon)
            {
                rasterDesc.FillMode = FillMode.Solid;
                context.Rasterizer.State = new RasterizerState(context.Device, rasterDesc);
            }


            if (!isPoligon)
            {
                rasterDesc.FillMode = FillMode.Wireframe;
                context.Rasterizer.State = new RasterizerState(context.Device, rasterDesc);
            }

        }

        private void Update()
        {
            foreach (var bindKey in _controlBinds.Keys)
            {
                if (KeyboardInput.GetKeyDown(bindKey))
                {
                    _controlBinds[bindKey]();
                }
            }
        }

        private static Scene InitializeScene()
        {
            string ShadersPath = "../../src/Shaders/";

            var scene = new Scene();

            #region Add cube

            GameObject gObj = new GameObject();

            gObj.AddComponent(new Transform(new Vector3(0f, 0f, 0f)));
            gObj.AddComponent(
                new Mesh(
                new Vertex[]
                {
                    new Vertex(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0.5f, 0f, 0f)),  // 0-Top-left
                    new Vertex(new Vector3(0.5f, 0.5f, -0.5f),  new Vector3(0.5f, 0f, 0f)),  // 1-Top-right
                    new Vertex(new Vector3(0.5f, -0.5f, -0.5f),  new Vector3(0.5f, 0f, 0f)), // 2-Base-right
                    new Vertex(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0.5f, 0f, 0f)), // 3-Base-left

                    new Vertex(new Vector3(-0.5f, 0.5f, 0.5f),  new Vector3(0.5f, 0f, 0f)),  // 4-Top-left
                    new Vertex(new Vector3(0.5f, 0.5f, 0.5f),   new Vector3(0.5f, 0f, 0f)),  // 5-Top-right
                    new Vertex(new Vector3(0.5f, -0.5f, 0.5f),  new Vector3(0.5f, 0f, 0f)),  // 6-Base-right
                    new Vertex(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0.5f, 0f, 0f)),
                },
                new ushort[]
                {
                    0, 1, 2, // Front A
                    0, 2, 3, // Front B
                    1, 5, 6, // Right A
                    1, 6, 2, // Right B
                    1, 0, 4, // Top A
                    1, 4, 5, // Top B
                    5, 4, 7, // Back A
                    5, 7, 6, // Back B
                    4, 0, 3, // Left A
                    4, 3, 7, // Left B
                    3, 2, 6, // Bottom A
                    3, 6, 7, // Bottom B
                }));

            var shaders = new Shader[]
            {
                new VertexShader(ShaderBytecode.CompileFromFile($"{ShadersPath}VS.hlsl", "VSMain", "vs_5_0")), 
                new PixelShader(ShaderBytecode.CompileFromFile($"{ShadersPath}PS.hlsl", "PSMain", "ps_5_0")),
        };

            gObj.AddComponent(new MeshRenderer(shaders));
            gObj.GetComponent<MeshRenderer>().Initialize();

            scene.AddObject(gObj);

            #endregion

            scene.Camera = CreateCamera();

            return scene;
        }

        private static GameObject CreateCamera()
        {
            var gObj = new GameObject();

            gObj.AddComponent(new Transform(new Vector3(0f, 0f, 10f)));
            gObj.AddComponent(new Camera());
            gObj.AddComponent(new CameraController());

            return gObj;
        }
    }
}
