using UnityEngine;

namespace EraSoren.Menu
{
    public abstract class MenuItemTypeManager : MonoBehaviour
    {
        public abstract MenuListItem Create(string itemName, Transform canvasMenuParent, Transform parentObject);
    }
}