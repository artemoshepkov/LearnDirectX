using DevExpress.Data.WcfLinq.Helpers;

namespace LearnDirectX.src.Common.Components
{
    public class GameObject
    {
        public string Name { get; set; } = "GameObject";
        public ComponentManager ComponentManager { get; }

        public GameObject()
        {
            ComponentManager = new ComponentManager();
        }

        public void AddComponent(Component component)
        {
            component.Owner = this;
            ComponentManager.AddComponent(component);
        }

        public T GetComponent<T>() where T : Component, new() => ComponentManager.GetComponent<T>();

        public override string ToString()
        {
            return Name;
        }
    }
}
