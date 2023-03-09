using System.Numerics;

namespace LearnDirectX.src.Common.Components.GridTask
{
    public class MaterialUpdater : Component
    {
        public MaterialUpdater() 
        {
        }

        public void Initialize()
        {
            var prop = Owner.GetComponent<FloatProperty>();
            prop.AddEventValueChanged(UpdateMaterial);
            UpdateMaterial(prop.Value);
        }

        public void UpdateMaterial(float prop)
        {
            var material = Owner.GetComponent<Material>();

            material.Color += new Vector4(prop / 1000);
        }
    }
}
