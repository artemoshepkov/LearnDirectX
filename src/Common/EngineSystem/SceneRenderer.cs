using LearnDirectX.src.Common.Components;
using LearnDirectX.src.Common.EngineSystem.Rendering;
using System;

namespace LearnDirectX.src.Common.EngineSystem
{
    public static class SceneRenderer
    {
        public static void Render(Scene scene)
        {
            CameraContext cameraContext = new CameraContext()
            {
                Camera = scene.Camera.GetComponent<Camera>(),
                Transform = scene.Camera.GetComponent<Transform>(),
            };

            RendererContext rendererContext = new RendererContext()
            {
                CameraContext = cameraContext,
                Lights = scene.Lights,
            };

            foreach (var gameObject in scene.GameObjects)
            {
                RenderObject(gameObject, rendererContext);
            }
        }

        public static void RenderObject(GameObject gObj, RendererContext rendererContext)
        {
            if (!gObj.IsEnabled)
                return;

            if (gObj.Children.Count > 0)
            {
                foreach (var child in gObj.Children)
                {
                    RenderObject(child, rendererContext);
                }
            }

            var meshRenderer = gObj.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.Render(rendererContext);
            }
        }
    }
}
