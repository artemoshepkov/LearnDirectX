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
        private Dictionary<Key, Action> _controlBinds;

        private bool isPoligon = false;

        public List<Scene> Scenes;

        public App() 
        {
            _controlBinds = new Dictionary<Key, Action>()
            {
                { Key.Escape, Window.Exit },
                { Key.R, Window.ChangeCursorMode },
                { Key.F, SetPoligonMode },
            };

            Engine.AddScene(InitializeScene());
            Engine.AddEventUpdate(Update);
        }

        private void SetPoligonMode()
        {
            var context = Window.Instance.Device.ImmediateContext;
            var rasterDesc = context.Rasterizer.State.Description;

            if (context.Rasterizer.State == null)
            {
                rasterDesc = new RasterizerStateDescription()
                {
                    CullMode = CullMode.Back,
                    FillMode = FillMode.Solid
                };
            }

            rasterDesc.FillMode = isPoligon ? FillMode.Solid : FillMode.Wireframe;

            context.Rasterizer.State = new RasterizerState(context.Device, rasterDesc);
            isPoligon = !isPoligon;
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
            var scene = new Scene();

            string ShadersPath = "../../src/Shaders/";

            var shadersObjects = new Shader[]
            {
                new VertexShader(ShaderBytecode.CompileFromFile($"{ShadersPath}VS.hlsl", "VSMain", "vs_5_0")),
                new PixelShader(ShaderBytecode.CompileFromFile($"{ShadersPath}PS.hlsl", "PSMain", "ps_5_0")),
            };

            GameObject gObj;

            #region Add cube

            gObj = new GameObject();

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

            gObj.AddComponent(new MeshRenderer(shadersObjects));
            gObj.GetComponent<MeshRenderer>().Initialize();

            scene.AddObject(gObj);

            #endregion

            #region Add light

            gObj = new GameObject();
            gObj.AddComponent(new DirectLight() { Color = new Vector4(1f), Direction = new Vector3(-0.2f, -1.0f, -0.3f) });
            scene.AddLight(gObj);

            #endregion

            scene.Camera = CreateCamera();

            return scene;
        }

        private static GameObject CreateCamera()
        {
            var gObj = new GameObject();

            gObj.AddComponent(new Transform(new Vector3(0f, 3f, 10f)));
            gObj.AddComponent(new Camera());
            gObj.AddComponent(new CameraController());

            return gObj;
        }
    }
}
