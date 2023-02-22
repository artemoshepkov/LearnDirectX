using LearnDirectX.src;
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

            var app = new App();

            Engine.Run();
        }
    }
}
