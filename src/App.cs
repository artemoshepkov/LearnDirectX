using LearnDirectX.src.Common.Components;
using LearnDirectX.src.Common.EngineSystem;
using SharpDX.D3DCompiler;
using System;
using System.Collections.Generic;
using System.Numerics;
using Key = SharpDX.DirectInput.Key;
using VertexShader = LearnDirectX.src.Common.EngineSystem.Shaders.VertexShader;
using PixelShader = LearnDirectX.src.Common.EngineSystem.Shaders.PixelShader;

using SharpDX.Direct3D11;
using LearnDirectX.src.Common;

namespace LearnDirectX.src
{
    public sealed class App
    {
        private Dictionary<Key, Action> _controlBinds;

        private bool isPoligon = false;

        public List<Scene> Scenes;

        public static readonly int NUM_POINT_LIGHTS = 2;

        public App() 
        {
            _controlBinds = new Dictionary<Key, Action>()
            {
                { Key.Escape, Window.Exit },
                { Key.R, () => { Window.ChangeCursorMode(); Engine.SwitchCameraOnOff(); } },
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
                    CullMode = CullMode.None,

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

            Shader[] shadersObjects;

            GameObject gObj;

            #region Add surface

            shadersObjects = new Shader[]
            {
                new VertexShader(ShaderBytecode.CompileFromFile($"{ShadersPath}VS.hlsl", "VSMain", "vs_5_0")),
                new PixelShader(ShaderBytecode.CompileFromFile($"{ShadersPath}PS.hlsl", "PSMain", "ps_5_0")),
            };

            var surfaceLoader = new MeshLoader();
            var quads = surfaceLoader.ReadCornersFromFile("../../Assets/surface1.txt");

            for (int i = 0; i < quads.Count; i++)
            {
                var vertexesSurface = new List<Common.EngineSystem.Shaders.Vertex>();
                var indexes = new List<ushort>();

                foreach (var point in quads[i].Points)
                {
                    vertexesSurface.Add(new Common.EngineSystem.Shaders.Vertex(point));
                }

                indexes.Add(0);
                indexes.Add(1);
                indexes.Add(2);

                indexes.Add(0);
                indexes.Add(2);
                indexes.Add(3);

                gObj = new GameObject();

                gObj.Name = "Quad" + i;
                gObj.AddComponent(new Transform(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0.005f, 0.005f, 0.005f)));

                gObj.AddComponent(
                    new Mesh()
                    {
                        Vertexes = vertexesSurface.ToArray(),
                        Indexes = indexes.ToArray(),
                    });

                gObj.AddComponent(new MeshRenderer(shadersObjects));
                gObj.GetComponent<MeshRenderer>().Initialize();

                gObj.AddComponent(new Material(new Vector4(0f, 0.6f, 0f, 1f)));
                gObj.AddComponent(new FloatProperty(1f));

                scene.AddObject(gObj);
            }

            #endregion

            #region Add light

            //gObj = new GameObject();
            //gObj.AddComponent(new DirectLight() { Color = new Vector4(1f), Direction = new Vector3(0f, -1f, 0f) });
            //scene.AddLight(gObj);

            gObj = new GameObject();
            gObj.AddComponent(new Transform(new Vector3(2f, 2f, 0f)));
            gObj.AddComponent(
                new PointLight() 
                { 
                    Color = new Vector4(1f),
                    Attenuation = new Common.EngineSystem.Shaders.Structures.Lights.Attenuation(1f, 0.7f, 1.8f)
                });
            scene.AddLight(gObj);


            gObj = new GameObject();
            gObj.AddComponent(new Transform(new Vector3(-2f, 2f, 0f)));
            gObj.AddComponent(
                new PointLight()
                {
                    Color = new Vector4(1f),
                    Attenuation = new Common.EngineSystem.Shaders.Structures.Lights.Attenuation(1f, 0.7f, 1.8f)
                });
            scene.AddLight(gObj);

            #endregion

            scene.Camera = CreateCamera();

            return scene;
        }

        private static GameObject CreateCamera()
        {
            var gObj = new GameObject();

            gObj.AddComponent(new Transform(new Vector3(0f, 20f, 0f)));
            gObj.AddComponent(new Camera());
            gObj.AddComponent(new CameraController());

            return gObj;
        }
    }
}
