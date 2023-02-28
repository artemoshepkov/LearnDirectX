using LearnDirectX.src;
using LearnDirectX.src.Common.EngineSystem;

namespace LearnDirectX
{
    public class Program
    {
        static void Main()
        {
            Engine.Init("Game", 1280, 800);

            var app = new App();

            Engine.Run();
        }
    }
}
