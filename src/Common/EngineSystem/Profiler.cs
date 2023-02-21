using System;
using System.Diagnostics;

namespace LearnDirectX.src.Common.EngineSystem
{
    public sealed class Profiler
    {
        private static Profiler _instance;
        private Stopwatch _stopwatch = new Stopwatch();
        private float _frameTimes;

        private Profiler() { }

        public static Profiler Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Profiler();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public static float FrameTimes => Instance._frameTimes;

        public static float DeltaTime => (float)Instance._stopwatch.Elapsed.TotalMilliseconds;

        public static void StartFrame() => Instance._stopwatch.Restart();

        public static void EndFrame()
        {
            Instance._stopwatch.Stop();
            Instance._frameTimes = (float)Instance._stopwatch.Elapsed.TotalMilliseconds;
        }

    }
}
