using LearnDirectX.src.Common.Components;
using System.Windows.Forms;

namespace LearnDirectX.src.View.Controls
{
    public class TreeNodeGameObject : TreeNode
    {
        public GameObject GameObjectItem;

        public TreeNodeGameObject(GameObject item)
        {
            GameObjectItem = item;
            Text= item.Name;
        }
    }
}
