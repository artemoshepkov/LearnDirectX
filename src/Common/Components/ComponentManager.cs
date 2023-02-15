using System;
using System.Collections.Generic;

namespace LearnDirectX.src.Common.Components
{
    public class ComponentManager
    {
        public Dictionary<Type, Component> Components { get; private set; }

        public ComponentManager() 
        {
            Components = new Dictionary<Type, Component>();
        }

        public void AddComponent<T>(T component) where T : Component
        {
            Components.Add(component.GetType(), component);
        }

        public void RemoveComponent<T>() where T :Component
        {
            Components.Remove(typeof(T));
        }

        public T GetComponent<T>() where T : Component
        {
            return Components.TryGetValue(typeof(T), out var value) ? (T)value : null;
        }
    }
}
