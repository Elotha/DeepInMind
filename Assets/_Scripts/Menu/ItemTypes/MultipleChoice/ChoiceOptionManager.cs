using EraSoren._Core.Helpers;
using EraSoren.Menu.General;
using EraSoren.Menu.ItemTypes.Button;
using EraSoren.Menu.Managers;
using TMPro;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.MultipleChoice
{
    public class ChoiceOptionManager : MenuItemTypeManager
    {
        public GameObject optionCanvasPrefab;
        public GameObject optionLogicPrefab;

        [SerializeField] private DefaultButtonProperties defaultButtonProperties;
        
        private GameObject CreateOptionCanvasObject(string optionName, MultipleChoiceItem multipleChoiceItem)
        {
            var optionItem = Instantiate(optionCanvasPrefab, multipleChoiceItem.menuCanvasObject.transform);
            optionItem.name = optionName;
            return optionItem;
        }

        public override bool CreateScript(string itemName)
        {
            return CreateMenuScript.I.CreateScript(itemName, nameof(MultipleChoiceOption), 
                MenuItemTypes.MultipleChoice);
        }

        public override void CreateObjects(string itemName, Transform parentObject)
        {
            var menuLogicObject = Instantiate(optionLogicPrefab, parentObject);
            menuLogicObject.name = itemName;
        }

        public override MenuItem Finalize(GameObject obj, string itemName, Transform canvasMenuParent)
        {
            var multipleChoiceItem = canvasMenuParent.GetComponent<MultipleChoiceItem>();
            var optionObject = CreateOptionCanvasObject(itemName, multipleChoiceItem);
            var newItem = optionObject.GetComponent<MultipleChoiceOption>();
            
            newItem.itemName = itemName;
            newItem.canvasObject = optionObject;
            newItem.canvasObject.GetComponent<ButtonInteract>().buttonItem = newItem;
            newItem.buttonComponent = optionObject.GetComponent<UnityEngine.UI.Button>();
            newItem.rectTransform = optionObject.GetComponent<RectTransform>();
            newItem.textComponent = optionObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            newItem.textComponent.font = FontManager.I.fontType;
            newItem.SetFontSize();
            newItem.lengthInHierarchy = LengthInHierarchyManager.I.lengthInHierarchy;
            newItem.buttonHeight = defaultButtonProperties.height;
            newItem.buttonWidth = defaultButtonProperties.width;

            FontManager.SetText(itemName, newItem.textComponent);
            
            itemsList.AddItem(newItem);

            return newItem;
        }
    }
}