using System.Threading.Tasks;
using EraSoren.Menu.General;
using EraSoren.Menu.Managers;
using TMPro;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.MultipleChoice
{
    public class MultipleChoiceManager : MenuItemTypeManager
    {
        [Header("Objects")]
        [SerializeField] private GameObject multipleChoiceCanvasPrefab;
        [SerializeField] private GameObject multipleChoiceLogicPrefab;
        
        [Header("References")] 
        public DefaultMultipleChoiceProperties defaultMultipleChoiceProperties;

        public override bool CreateScript(string itemName)
        {
            return CreateMenuScript.I.CreateScript(itemName, nameof(MultipleChoiceItem), 
                MenuItemTypes.MultipleChoice);
        }

        public override void CreateObjects(string itemName, Transform parentObject)
        {
            var menuLogicObject = Instantiate(multipleChoiceLogicPrefab, parentObject);
            menuLogicObject.name = itemName;
        }

        public override MenuItem Finalize(GameObject obj, string itemName, Transform canvasMenuParent)
        {
            var multipleChoiceObj = CreateMultipleChoiceObject(itemName, canvasMenuParent);
            
            var newItem = obj.GetComponent<MultipleChoiceItem>();
            newItem.itemName = itemName;
            newItem.textComponent = multipleChoiceObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            newItem.textComponent.font = FontManager.I.fontType;
            newItem.rectTransform = multipleChoiceObj.transform.GetComponent<RectTransform>();
            newItem.canvasObject = multipleChoiceObj;
            newItem.SetFontSize();
            newItem.lengthInHierarchy = LengthInHierarchyManager.I.lengthInHierarchy;
            newItem.GetComponent<OptionCreator>().multipleChoiceItem = newItem;
            newItem.menuCanvasObject = obj.transform.parent.GetComponent<MenuItemCreator>().canvasMenuParent.gameObject;
            
            itemsList.AddItem(newItem);
        
            FontManager.SetText(itemName, newItem.textComponent);
        
            return newItem;
        }

        private GameObject CreateMultipleChoiceObject(string itemName, Transform parent)
        {
            var multipleChoiceObj = Instantiate(multipleChoiceCanvasPrefab, parent);
            multipleChoiceObj.name = itemName;
            return multipleChoiceObj;
        }

        public void DestroyMultipleChoiceItem(MultipleChoiceItem multipleChoiceItem)
        {
            itemsList.RemoveItem(multipleChoiceItem);
        }
    }
}