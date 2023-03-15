using LearnDirectX.src;
using LearnDirectX.src.Common.EngineSystem;

namespace LearnDirectX
{
    public class Program
    {
        static void Main()
        {
            Engine.Init("Game", 1980, 1020);

            var app = new App();

            Engine.Run();
        }
    }
}
