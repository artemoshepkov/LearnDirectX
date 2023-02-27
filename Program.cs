using LearnDirectX.src;
using LearnDirectX.src.Common.EngineSystem;

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
