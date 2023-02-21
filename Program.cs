using LearnDirectX.src.Common.Components;
using LearnDirectX.src.Common.EngineSystem;
using LearnDirectX.src.Common.EngineSystem.Rendering;
using SharpDX.D3DCompiler;
using System.Numerics;

namespace LearnDirectX
{
    public class Program
    {
        static void Main()
        {
            Engine.Init("Game", 640, 480);

            Engine.GetInstance().AddRenderLayer(new DirectXSceneRenderer(InitializeScene()));

            Engine.Run();
        }

        private static GameObject CreateCamera()
        {
            var gObj = new GameObject();

            gObj.AddComponent(new Transform(new Vector3(0f, 0f, -10f)));
            gObj.AddComponent(new Camera());
            gObj.AddComponent(new CameraController());

            return gObj;
        }

        private static Scene InitializeScene()
        {
            string ShadersPath = "../../src/Shaders/";

            var scene = new Scene();

            GameObject gObj = new GameObject();

            gObj.AddComponent(new Transform(new Vector3(1f, 1f, 1f)));
            gObj.AddComponent(new Mesh(
                new Vector4[]
                {
                    new Vector4(0.0f, 0.5f, 0.5f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                    new Vector4(0.5f, -0.5f, 0.5f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                    new Vector4(-0.5f, -0.5f, 0.5f, 1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f)
                }));
            var vsbc = ShaderBytecode.CompileFromFile($"{ShadersPath}vertex.hlsl", "VS", "vs_4_0", ShaderFlags.None, EffectFlags.None);
            var psbc = ShaderBytecode.CompileFromFile($"{ShadersPath}pixel.hlsl", "PS", "ps_4_0", ShaderFlags.None, EffectFlags.None);
            gObj.AddComponent(new MeshRenderer(new Shader(vsbc, psbc)));
            gObj.GetComponent<MeshRenderer>().Initialize();

            scene.AddObject(gObj);

            scene.Camera = CreateCamera();

            return scene;
        }
    }
}
