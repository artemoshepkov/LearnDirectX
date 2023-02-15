using LearnDirectX.src.Common.Components;

namespace LearnDirectX.src.Common.EngineSystem.Rendering
{
    public sealed class DirectXSceneRenderer : RenderLayer
    {
        private Scene _associatedScene;

        public DirectXSceneRenderer(Scene scene)
        {
            _associatedScene= scene;
        }

        public override void OnBegin()
        {

        }

        public override void OnEnd()
        {

        }

        public override void OnDraw()
        {
            foreach (var gameObject in _associatedScene.GameObjects)
            {
                var meshRenderer = gameObject.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.Render();
                }
            }
        }

    }
}
