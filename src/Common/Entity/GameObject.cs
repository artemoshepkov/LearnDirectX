using DevExpress.XtraTabbedMdi;
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
            child.Parent = this;
            Children.Add(child);
        }

        public void RemoveChild(GameObject child)
        {
            child.Parent = null;
            Children.Remove(child);
        }

        public void AddComponent(Component component)
        {
            component.Owner = this;
            component.Start();
            ComponentManager.AddComponent(component);
        }

        public T GetComponent<T>() where T : Component, new() => ComponentManager.GetComponent<T>();

        public override string ToString()
        {
            return Name;
        }
    }
}
