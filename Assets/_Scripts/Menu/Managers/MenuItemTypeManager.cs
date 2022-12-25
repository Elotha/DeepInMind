using System.Collections.Generic;
using System.Threading.Tasks;
using EraSoren.Menu.General;
using EraSoren.Menu.ItemTypes;
using UnityEngine;

namespace EraSoren.Menu.Managers
{
    [ExecuteInEditMode]
    public abstract class MenuItemTypeManager : MonoBehaviour
    {
        public abstract void CreateScript(string itemName);
        public abstract void CreateObjects(string itemName, Transform parentObject);
        public abstract MenuItem Finalize(GameObject obj, string itemName, Transform canvasMenuParent);

        public DefaultProperties defaultProperties;

        public ItemsList itemsList;

        public static void ChangeLengthInHierarchy(MenuItem item, int lengthInHierarchy)
        {
            item.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lengthInHierarchy);
        }
    }
}