using LearnDirectX.src.Common.Components;
using System.Collections.Generic;

namespace LearnDirectX.src.Common.EngineSystem
{
    public class Scene
    {
        public GameObject Camera;
        public List<GameObject> GameObjects { get; private set; }

        public Scene() 
        { 
            GameObjects = new List<GameObject>();
        }

        public void AddObject(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
        }

        public void RemoveObject(GameObject gameObject)
        {
            GameObjects.Remove(gameObject);
        }
    }
}
