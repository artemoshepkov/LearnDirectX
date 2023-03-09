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
using LearnDirectX.src.Common.Geometry;
using System.Linq;
using LearnDirectX.src.Common.Components.GridTask;

namespace LearnDirectX.src
{
    public sealed class App
    {
        private Dictionary<Key, Action> _controlBinds;

        private bool isPoligon = false;
        
        private Dictionary<string, List<QuadProperty>> _gridProperties;

        public List<Scene> Scenes;

        public App() 
        {
            _controlBinds = new Dictionary<Key, Action>()
            {
                { Key.Escape, Window.Exit },
                { Key.R, () => { Window.ChangeCursorMode(); Engine.SwitchCameraOnOff(); } },
                { Key.F, SetPoligonMode },
            };

            Engine.AddScene(InitializeGridScene());
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

        private Scene InitializeGridScene()
        {
            string ShadersPath = "../../src/Shaders/";
            var gridPath = "../../Assets/grid.bin";
            var gridPropsPath = "../../Assets/gridProps.txt";


            var scene = new Scene();

            scene.Camera = CreateCamera();


            #region Add grid

            Shader[] shadersObjects = new Shader[]
            {
                new VertexShader(ShaderBytecode.CompileFromFile($"{ShadersPath}VS.hlsl", "VSMain", "vs_5_0")),
                new PixelShader(ShaderBytecode.CompileFromFile($"{ShadersPath}PS.hlsl", "PSMain", "ps_5_0")),
            };

            var grid = GridLoader.ReadGridFromFile(gridPath);

            _gridProperties = GridLoader.ReadPropsFromFile(grid.Size, gridPropsPath);

            var gridGameObject = new GameObject();

            gridGameObject.Name = "Grid";
            gridGameObject.AddComponent(new Transform(new Vector3(0, 0, 0)));
            gridGameObject.AddComponent(new Common.Components.GridTask.Grid(grid.Size));
            gridGameObject.AddComponent(new SliceRenderer(grid.Size));

            foreach (var quad in grid.Quads)
            {
                var propName = _gridProperties.Keys.Last();

                var gObj = new GameObject();

                gObj.Name = "Quad: " + quad.ToString();
                gObj.IsEnabled = quad.IsActive;

                gObj.AddComponent(new Transform(new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0.01f, 0.01f, 0.01f)));

                gObj.AddComponent(new Material(new Vector4(0.35f, 0.35f, 0f, 1f)));

                gObj.AddComponent(new QuadIndex(quad.I, quad.J, quad.K));

                foreach (var prop in _gridProperties[propName])
                {
                    if (prop.Indexes.X == quad.I && prop.Indexes.Y == quad.J && prop.Indexes.Z == quad.K)
                    {
                        gObj.AddComponent(new FloatProperty(prop.Value));
                    }
                }
                gObj.AddComponent(new MaterialUpdater());
                gObj.GetComponent<MaterialUpdater>().Initialize();


                var vertexes = new List<Common.EngineSystem.Shaders.Vertex>();

                for (int i = 0; i < 4; i++)
                {
                    vertexes.Add(new Common.EngineSystem.Shaders.Vertex(quad.TopCorners[i]));
                    vertexes.Add(new Common.EngineSystem.Shaders.Vertex(quad.BottomCorners[i]));
                }

                gObj.AddComponent(new Mesh(
                    vertexes.ToArray(),
                    new ushort[]
                    {
                        0,2,6,
                        2,6,4,

                        0,3,2,
                        0,3,1,

                        0,1,7,
                        0,7,6,

                        5,7,3,
                        7,3,1,

                        5,7,4,
                        7,4,6,

                        5,3,2,
                        5,2,4,
                    }
                ));
                gObj.AddComponent(new MeshRenderer(shadersObjects));
                gObj.GetComponent<MeshRenderer>().Initialize();

                gridGameObject.AddChild(gObj);
            }

            gridGameObject.GetComponent<SliceRenderer>().UpdateSlices();

            scene.AddObject(gridGameObject);

            #endregion

            return scene;
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

            var surfaceLoader = new SurfaceLoader();
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


        private void ChangeGridProperty(string prop)
        {

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
