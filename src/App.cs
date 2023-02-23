using LearnDirectX.src.Common.Components;
using LearnDirectX.src.Common.EngineSystem;
using LearnDirectX.src.Common.EngineSystem.Rendering;
using LearnDirectX.src.Common.EngineSystem.Shaders;
using SharpDX.D3DCompiler;
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Numerics;

using EffectFlags = SharpDX.D3DCompiler.EffectFlags;

namespace LearnDirectX.src
{
    public sealed class App
    {
        private DirectXSceneRenderer _renderer;
        private Dictionary<Key, Action> _controlBinds;

        public List<Scene> Scenes;

        public App() 
        {
            _renderer = new DirectXSceneRenderer(InitializeScene());

            _controlBinds = new Dictionary<Key, Action>()
            {
                { Key.Escape, Window.Exit },
                { Key.R, Window.ChangeCursorMode },
            };

            Engine.AddRenderLayer(_renderer);
            Engine.AddEventUpdate(Update);
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

            GameObject gObj = new GameObject();

            gObj.AddComponent(new Transform(new Vector3(1f, 1f, 1f)));
            gObj.AddComponent(
                new Mesh(
                new Vertex[]
                {
                    new Vertex(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(1f, 0.5f, 0.5f)),  // 0-Top-left
                    new Vertex(new Vector3(0.5f, 0.5f, -0.5f),  new Vector3(1f, 0.5f, 0.5f)),  // 1-Top-right
                    new Vertex(new Vector3(0.5f, -0.5f, -0.5f),  new Vector3(1f, 0.5f, 0.5f)), // 2-Base-right
                    new Vertex(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(1f, 0.5f, 0.5f)), // 3-Base-left

                    new Vertex(new Vector3(-0.5f, 0.5f, 0.5f),  new Vector3(0.5f, 0.5f, 0.5f)),  // 4-Top-left
                    new Vertex(new Vector3(0.5f, 0.5f, 0.5f),   new Vector3(0.5f, 0.5f, 0.5f)),  // 5-Top-right
                    new Vertex(new Vector3(0.5f, -0.5f, 0.5f),  new Vector3(0.5f, 0.5f, 0.5f)),  // 6-Base-right
                    new Vertex(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f)),
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

            var vsbc = ShaderBytecode.CompileFromFile($"{ShadersPath}vertex.hlsl", "VS", "vs_4_0", ShaderFlags.None, EffectFlags.None);
            var psbc = ShaderBytecode.CompileFromFile($"{ShadersPath}pixel.hlsl", "PS", "ps_4_0", ShaderFlags.None, EffectFlags.None);
            gObj.AddComponent(new MeshRenderer(new Shader(vsbc, psbc)));
            gObj.GetComponent<MeshRenderer>().Initialize();

            scene.AddObject(gObj);

            scene.Camera = CreateCamera();

            return scene;
        }

        private static GameObject CreateCamera()
        {
            var gObj = new GameObject();

            gObj.AddComponent(new Transform(new Vector3(0f, 0f, -10f)));
            gObj.AddComponent(new Camera());
            gObj.AddComponent(new CameraController());

            return gObj;
        }
    }

    public class CubeMeshGenerator
    {
        public static Vector4[] GenerateVertixes() => new[] { new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f), // Front
                                      new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                                      new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                                      new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                                      new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                                      new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),

                                      new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f), // BACK
                                      new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                                      new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                                      new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                                      new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                                      new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),

                                      new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f), // Top
                                      new Vector4(-1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                                      new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                                      new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                                      new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                                      new Vector4( 1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),

                                      new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f), // Bottom
                                      new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                                      new Vector4(-1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                                      new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                                      new Vector4( 1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                                      new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),

                                      new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f), // Left
                                      new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                                      new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                                      new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                                      new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                                      new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),

                                      new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f), // Right
                                      new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                                      new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                                      new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                                      new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                                      new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f), };

        
    }
}
