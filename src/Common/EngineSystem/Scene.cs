using LearnDirectX.src.Common.Components;
using System.Collections.Generic;

namespace LearnDirectX.src.Common.EngineSystem
{
    public class Scene
    {
        public string Name;

        public GameObject Camera;
        public List<GameObject> GameObjects { get; private set; }
        public List<GameObject> Lights { get; private set; }

        public Scene(string name)
        {
            GameObjects = new List<GameObject>();
            Lights = new List<GameObject>();
            Name = name;
        }

        public void AddObject(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
        }

        public void RemoveObject(GameObject gameObject)
        {
            GameObjects.Remove(gameObject);
        }

        public void AddLight(GameObject gameObject)
        {
            Lights.Add(gameObject);
        }

        public void RemoveLight(GameObject gameObject)
        {
            Lights.Remove(gameObject);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
