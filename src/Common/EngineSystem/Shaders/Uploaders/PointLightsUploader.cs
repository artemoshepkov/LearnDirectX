using LearnDirectX.src.Common.Components;
using LearnDirectX.src.Common.EngineSystem.Rendering;
using LearnDirectX.src.Common.EngineSystem.Shaders.Buffers;
using System.Collections.Generic;

namespace LearnDirectX.src.Common.EngineSystem.Shaders.Uploaders
{
    public sealed class PointLightsUploader : ShaderBufferUploader
    {
        private ArrayConstantBuffer<Structures.Lights.PointLight> _arrayConstantBuffer;

        public PointLightsUploader(int arraySize, int registerNumber) : base(registerNumber)
        {
            _arrayConstantBuffer = new ArrayConstantBuffer<Structures.Lights.PointLight>(arraySize);
        }

        public override void Upload(GameObject gameObject, RendererContext rendererContext)
        {
            var immediateContext = Window.Instance.Device.ImmediateContext;

            immediateContext.PixelShader.SetConstantBuffer(_registerNumber, _arrayConstantBuffer.Buffer);

            var pointLights = new List<Structures.Lights.PointLight>();

            foreach (var light in rendererContext.Lights)
            {
                var pointLight = light.GetComponent<PointLight>();
                var material = light.GetComponent<Material>();

                if (pointLight != null)
                {
                    var pointLightPosition = pointLight.Owner.GetComponent<Components.Transform>().Position;

                    pointLights.Add(
                        new Structures.Lights.PointLight()
                        {
                            Color = material.Color,
                            Position = pointLightPosition,
                            Attenuation = pointLight.Attenuation,
                        });
                }
            }

            _arrayConstantBuffer.UpdateArray(pointLights.ToArray());
        }
    }
}
