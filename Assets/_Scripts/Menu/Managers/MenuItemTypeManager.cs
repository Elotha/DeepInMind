using EraSoren.Menu.ItemTypes;
using UnityEngine;

namespace EraSoren.Menu.Managers
{
    public abstract class MenuItemTypeManager : MonoBehaviour
    {
        public abstract MenuItem Finalize(GameObject obj, string itemName, Transform canvasMenuParent);
        public abstract void CreateScript(string itemName);
        public abstract void CreateObjects(string itemName, Transform parentObject);
    }
}