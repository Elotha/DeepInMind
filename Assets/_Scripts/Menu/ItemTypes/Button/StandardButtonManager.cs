using System.Threading.Tasks;
using EraSoren.Menu.General;
using EraSoren.Menu.Managers;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Button
{
    public class StandardButtonManager : MenuItemTypeManager
    {
        [SerializeField] private GameObject standardButtonLogicPrefab;

        public override void CreateScript(string itemName)
        {
            CreateMenuScript.I.Create(itemName, nameof(StandardButtonItem), 
                MenuItemTypes.StandardButton);
        }

        public override void CreateObjects(string itemName, Transform parentObject)
        {
            var menuLogicObject = Instantiate(standardButtonLogicPrefab, parentObject);
            menuLogicObject.name = itemName;
        }
        
        public override MenuItem Finalize(GameObject obj, string itemName, Transform canvasMenuParent)
        {
            var newItem = ButtonManager.I.FinalizeItem(obj, itemName, canvasMenuParent);
            return newItem;
        }
    }
}