using System.Collections.Generic;

namespace LearnDirectX.src.Common.Components
{
    public class GameObject
    {
        public string Name { get; set; } = "GameObject";
        public bool IsEnabled = true;
        public ComponentManager ComponentManager { get; }
        public GameObject Parent { get; private set; }
        public List<GameObject> Children { get; private set; }


        public GameObject()
        {
            Parent = null;
            Children = new List<GameObject>();
            ComponentManager = new ComponentManager();
        }


        public void AddChild(GameObject child)
        {
            Children.Add(child);
            child.Parent = this;
        }

        public void RemoveChild(GameObject child)
        {
            Children.Remove(child);
            child.Parent = null;
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
