using EraSoren._Core.Helpers;
using EraSoren.Menu.General;
using EraSoren.Menu.Managers;
using TMPro;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Button
{
    public class ButtonManager : Singleton<ButtonManager>
    {
        [Header("Objects")]
        [SerializeField] private GameObject buttonPrefabDefault;
        [SerializeField] private GameObject buttonPrefabBack;
        
        [Header("References")] 
        public DefaultButtonProperties defaultButtonProperties;
        public ItemsList itemsList;

        public const string BackString = "Back";

        public MenuItem FinalizeItem(GameObject obj, string itemName, Transform canvasMenuParent)
        {
            var buttonObj = CreateButtonObject(itemName == BackString ?
                                             buttonPrefabBack : buttonPrefabDefault, itemName, canvasMenuParent);
            
            var newItem = obj.GetComponent<ButtonItem>();
            
            newItem.itemName = itemName;
            newItem.canvasObject = buttonObj;
            newItem.canvasObject.GetComponent<ButtonInteract>().buttonItem = newItem;
            newItem.buttonComponent = buttonObj.GetComponent<UnityEngine.UI.Button>();
            newItem.rectTransform = buttonObj.GetComponent<RectTransform>();
            newItem.textComponent = buttonObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            newItem.textComponent.font = FontManager.I.fontType;
            newItem.SetFontSize();
            newItem.totalHeight = TotalHeightManager.I.totalHeight;
            newItem.buttonHeight = defaultButtonProperties.height;
            newItem.buttonWidth = defaultButtonProperties.width;

            FontManager.SetText(itemName, newItem.textComponent);
            
            itemsList.AddItem(newItem);

            return newItem;
        }

        private static GameObject CreateButtonObject(GameObject prefabType, string buttonName, Transform canvasMenuParent)
        {
            var obj = Instantiate(prefabType, canvasMenuParent);
            obj.name = buttonName;
            return obj;
        }
        
        public static void ChangeButtonWidth(MenuItem item, int buttonWidth)
        {
            item.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonWidth);
        }

        public static void ChangeButtonHeight(MenuItem item, int buttonHeight)
        {
            item.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonHeight);
        }

        public void DestroyButtonItem(ButtonItem buttonItem)
        {
            itemsList.RemoveItem(buttonItem);
        }
    }
}