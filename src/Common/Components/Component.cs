using LearnDirectX.src.Common.Components;
using System.Windows.Forms;

namespace LearnDirectX.src.Common
{
    public class Component
    {
        public GameObject Owner { get; set; } = null;

        public virtual void Start() { }

        public virtual GroupBox GetGUI() 
        {
            return null;
        }
    }
}
