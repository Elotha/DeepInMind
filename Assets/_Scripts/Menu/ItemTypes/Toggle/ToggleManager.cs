using System.Threading.Tasks;
using EraSoren.Menu.General;
using EraSoren.Menu.ItemTypes.Button;
using EraSoren.Menu.Managers;
using TMPro;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Toggle
{
    public class ToggleManager : MenuItemTypeManager
    {
        [Header("Objects")]
        [SerializeField] private GameObject toggleCanvasPrefab;
        [SerializeField] private GameObject toggleLogicPrefab;
        
        [Header("References")] 
        public DefaultToggleProperties defaultToggleProperties;

        public override void CreateScript(string itemName)
        {
            CreateMenuScript.I.Create(itemName, nameof(ToggleItem), MenuItemTypes.Toggle);
        }

        public override void CreateObjects(string itemName, Transform parentObject)
        {
            var menuLogicObject = Instantiate(toggleLogicPrefab, parentObject);
            menuLogicObject.name = itemName;
        }

        public override MenuItem Finalize(GameObject obj, string itemName, Transform canvasMenuParent)
        {
            var toggleObj = CreateToggleObject(toggleCanvasPrefab, itemName, canvasMenuParent);
            
            var newItem = obj.GetComponent<ToggleItem>();
            newItem.itemName = itemName;
            newItem.textComponent = toggleObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            newItem.textComponent.font = FontManager.I.fontType;
            newItem.rectTransform = toggleObj.transform.GetComponent<RectTransform>();
            newItem.canvasObject = toggleObj;
            newItem.canvasObject.GetComponent<ToggleInteract>().toggleItem = newItem;
            newItem.SetFontSize();
            newItem.lengthInHierarchy = LengthInHierarchyManager.I.LengthInHierarchy;
            newItem.labelWidth = defaultToggleProperties.labelWidth;
                
            FontManager.SetText(itemName, newItem.textComponent);
            
            itemsList.AddItem(newItem);
            
            return newItem;
        }
        private static GameObject CreateToggleObject(GameObject prefabType, string buttonName, Transform canvasMenuParent)
        {
            var obj = Instantiate(prefabType, canvasMenuParent);
            obj.name = buttonName.Replace(" ", "");;
            return obj;
        }
        
        public static void ChangeLabelWidth(MenuItem item, int labelWidth)
        {
            item.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, labelWidth);
        }

        public void DestroyToggleItem(ToggleItem toggleItem)
        {
            itemsList.RemoveItem(toggleItem);
        }
    }
}