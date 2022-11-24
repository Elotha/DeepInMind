using UnityEngine;

namespace EraSoren.Menu.Managers
{
    public class StandardButtonManager : ButtonManager
    {
        [SerializeField] private GameObject standardButtonLogicPrefab;
        
        public override MenuListItem Create(string itemName, Transform canvasMenuParent, Transform parentObject)
        {
            var newItem = base.Create(itemName, canvasMenuParent, parentObject);
            var menuLogicObject = Instantiate(standardButtonLogicPrefab, parentObject);
            menuLogicObject.name = itemName;
            return newItem;
        }
    }
}