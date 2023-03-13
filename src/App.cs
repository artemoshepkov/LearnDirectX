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
using LearnDirectX.src.Common;
using LearnDirectX.src.Common.EngineSystem.Shaders.Uploaders;
using SharpDX.Direct2D1.Effects;

namespace LearnDirectX.src
{
    public sealed class App : BaseApplication
    {
        private bool _isPoligon = false;

        public App() 
        {
            _controlBinds = new Dictionary<Key, Action>()
            {
                { Key.Escape, Window.Exit },
                { Key.E, () => { Window.ChangeCursorMode(); Engine.SwitchCameraOnOff(); } },
                { Key.F, SetPoligonMode },
            };

            //Engine.AddScene(InitializeSurfaceScene());
            Engine.AddScene(InitializeLightScene());
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

            rasterDesc.FillMode = _isPoligon ? FillMode.Solid : FillMode.Wireframe;
            rasterDesc.IsMultisampleEnabled = _isPoligon;
            rasterDesc.IsAntialiasedLineEnabled = _isPoligon;

            context.Rasterizer.State = new RasterizerState(context.Device, rasterDesc);
            _isPoligon = !_isPoligon;
        }

        private Scene InitializeGridScene()
        {
            string ShadersPath = "../../src/Shaders/";
            var gridPath = "../../Assets/grid.bin";
            var gridPropsPath = "../../Assets/gridProps.txt";

            var scene = new Scene("Grid");

            scene.Camera = CreateCamera(0.01f);

            #region Add grid

            Shader[] shadersObjects = new Shader[]
            {
                new VertexShader(ShaderBytecode.CompileFromFile($"{ShadersPath}VS.hlsl", "VSMain", "vs_5_0")),
                new PixelShader(ShaderBytecode.CompileFromFile($"{ShadersPath}PS.hlsl", "PSMain", "ps_5_0")),
            };

            ShaderBufferUploader[] shadersUploaders = new ShaderBufferUploader[]
            {
                new FrameUploader(1),
                new MaterialUploader(2),
                new ObjectUploader(0),
            };

            var grid = GridLoader.ReadGridFromFile(gridPath);

            var gridProperties = GridLoader.ReadPropsFromFile(grid.Size, gridPropsPath);
            var gridHeightProperty = new GridProperty("Height");
            gridHeightProperty.MinValue = 0;
            gridHeightProperty.MaxValue = 13;
            foreach (var quad in grid.Quads)
            {
                gridHeightProperty.QuadProperties.Add(new QuadProperty(new Vector3(quad.I, quad.J, quad.K), quad.K));
            }
            gridProperties.Add(gridHeightProperty);

            var gridGameObject = new GameObject();

            gridGameObject.Name = "Grid";
            gridGameObject.AddComponent(new Transform());
            gridGameObject.GetComponent<Transform>().Rotate(new Vector3(0f, 0f, 180f));
            gridGameObject.GetComponent<Transform>().Scale(new Vector3(0.001f, 0.001f, 0.001f));
            gridGameObject.AddComponent(new GridProperties(gridProperties));
            gridGameObject.AddComponent(new Common.Components.GridTask.Grid(grid.Size));
            gridGameObject.AddComponent(new SliceRenderer());
            gridGameObject.AddComponent(new Palette(new List<Vector4>() { new Vector4(1f, 0f, 0f, 1f), new Vector4(0f, 1f, 0f, 1f) , new Vector4(0f, 0f, 1f, 1f) }));

            foreach (var quad in grid.Quads)
            {
                var gObj = new GameObject();

                gridGameObject.AddChild(gObj);

                gObj.Name = "Quad: " + quad.ToString();
                gObj.IsEnabled = quad.IsActive;

                gObj.AddComponent(new Transform());
                gObj.AddComponent(new Material(new Vector4(0f, 0.6f, 0f, 1f)));

                gObj.AddComponent(new QuadIndex(quad.I, quad.J, quad.K));
                gObj.AddComponent(new FloatProperty(0, 0, 0));
                gObj.AddComponent(new MaterialUpdater());

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
                gObj.AddComponent(new MeshRenderer(shadersObjects, shadersUploaders));

                var gObjGeometry = new GameObject();
                gObj.AddChild(gObjGeometry);
                gObjGeometry.AddComponent(new Transform());
                gObjGeometry.AddComponent(new Material(new Vector4(0f, 0f, 0f, 1f)));
                gObjGeometry.AddComponent(new Mesh(
                    vertexes.ToArray(),
                    new ushort[]
                    {
                        0,1,
                        2,3,
                        4,5,
                        6,7,

                        0,2,
                        0,6,
                        2,4,
                        4,6,

                        1,3,
                        1,7,
                        3,5,
                        5,7,
                    },
                    SharpDX.Direct3D.PrimitiveTopology.LineList));
                gObjGeometry.AddComponent(new MeshRenderer(shadersObjects, shadersUploaders));

            }

            gridGameObject.GetComponent<GridProperties>().UpdateProps();

            gridGameObject.GetComponent<SliceRenderer>().UpdateSlices();

            scene.AddObject(gridGameObject);

            #endregion

            return scene;
        }

        private static Scene InitializeSurfaceScene()
        {
            var scene = new Scene("Surface");

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
                gObj.AddComponent(new Transform());
                gObj.GetComponent<Transform>().Scale(new Vector3(0.005f, 0.005f, 0.005f));

                gObj.AddComponent(
                    new Mesh()
                    {
                        Vertexes = vertexesSurface.ToArray(),
                        Indexes = indexes.ToArray(),
                    });

                gObj.AddComponent(new MeshRenderer(shadersObjects, null));

                gObj.AddComponent(new Material(new Vector4(0f, 0.6f, 0f, 1f)));

                scene.AddObject(gObj);
            }

            #endregion

            scene.Camera = CreateCamera(0.01f);

            return scene;
        }

        private static Scene InitializeLightScene()
        {
            var scene = new Scene("Light");

            string ShadersPath = "../../src/Shaders/";

            Shader[] shadersObjects;

            ShaderBufferUploader[] shadersUploaders;

            GameObject gObj;

            #region Add cube

            shadersObjects = new Shader[]
            {
                new VertexShader(ShaderBytecode.CompileFromFile($"{ShadersPath}VS.hlsl", "VSMain", "vs_5_0")),
                new PixelShader(ShaderBytecode.CompileFromFile($"{ShadersPath}PSLight.hlsl", "PSMain", "ps_5_0")),
            };

            shadersUploaders = new ShaderBufferUploader[]
            {
                new PointLightsUploader(4, 3),
                new FrameUploader(1),
                new MaterialUploader(2),
                new ObjectUploader(0),
            };

            gObj = new GameObject();

            gObj.Name = "Cube";
            gObj.AddComponent(new Transform());
            gObj.GetComponent<Transform>().Scale(new Vector3(10f, 1f, 10f));

            gObj.AddComponent(
                new Mesh()
                {
                    Vertexes = CubeMeshGenerator.GenerateVertexes(5, 1),
                    Indexes = CubeMeshGenerator.GenerateIndexes(5),
                });
            gObj.AddComponent(new MeshRenderer(shadersObjects, shadersUploaders));
            gObj.AddComponent(new Material(new Vector4(1f)));

            scene.AddObject(gObj);

            #endregion

            #region Add light

            //gObj = new GameObject();
            //gObj.AddComponent(new DirectLight() { Color = new Vector4(1f), Direction = new Vector3(0f, -1f, 0f) });
            //scene.AddLight(gObj);

            scene.AddLight(CreatePointLight(new Vector3(2f, 2f, 0f), new Vector4(0f, 1f, 0f, 1f), new Common.EngineSystem.Shaders.Structures.Lights.Attenuation(1f, 0.7f, 1.8f)));
            scene.AddLight(CreatePointLight(new Vector3(-2f, 2f, 0f), new Vector4(1f, 0f, 0f, 1f), new Common.EngineSystem.Shaders.Structures.Lights.Attenuation(1f, 0.7f, 1.8f)));
            scene.AddLight(CreatePointLight(new Vector3(0f, 2f, 2f), new Vector4(0f, 0f, 1f, 1f), new Common.EngineSystem.Shaders.Structures.Lights.Attenuation(1f, 0.7f, 1.8f)));
            scene.AddLight(CreatePointLight(new Vector3(0f, 2f, -2f), new Vector4(1f), new Common.EngineSystem.Shaders.Structures.Lights.Attenuation(1f, 0.7f, 1.8f)));

            #endregion

            scene.Camera = CreateCamera(0.3f);

            return scene;
        }

        private static GameObject CreateCamera(float mouseSensitivity)
        {
            var gObj = new GameObject();

            gObj.AddComponent(new Transform());
            gObj.GetComponent<Transform>().Translate(new Vector3(0f, 3f, -4f));
            gObj.AddComponent(new Camera());
            gObj.AddComponent(new CameraController());
            gObj.GetComponent<CameraController>().MouseSensitivity = mouseSensitivity;

            return gObj;
        }

        private static GameObject CreatePointLight(Vector3 position, Vector4 color, Common.EngineSystem.Shaders.Structures.Lights.Attenuation attenuation)
        {
            var gObj = new GameObject();

            gObj.Name = "Light";
            gObj.AddComponent(new Transform());
            gObj.GetComponent<Transform>().Translate(position);
            gObj.AddComponent(new Material(color));
            gObj.AddComponent(
                new PointLight()
                {
                    Attenuation = attenuation
                });

            return gObj;
        }
    }
}
