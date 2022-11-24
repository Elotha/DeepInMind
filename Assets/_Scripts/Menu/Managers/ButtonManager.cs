using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EraSoren.Menu.Managers
{
    public class ButtonManager : MenuItemTypeManager
    {
        [Header("Objects")]
        [SerializeField] private GameObject buttonPrefabDefault;
        [SerializeField] private GameObject buttonPrefabBack;
        
        [Header("References")] 
        [SerializeField] private DefaultButtonProperties defaultButtonProperties;
        [SerializeField] private ButtonItemsList buttonItemsList;

        public const string BackString = "Back";

        public override MenuListItem Create(string itemName, Transform canvasMenuParent, Transform parentObject)
        {
            var obj = CreateButtonObject(itemName == BackString ?
                                             buttonPrefabBack : buttonPrefabDefault, itemName, canvasMenuParent);
            var buttonComponent = obj.GetComponent<Button>();
            var rectTransform = obj.GetComponent<RectTransform>();
            var buttonText = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            
            var newItem = new ButtonListItem
            {
                buttonComponent = buttonComponent,
                itemTextComponent = buttonText,
                itemName = itemName
            };

            newItem.itemTextComponent.font = FontManager.I.fontType;
            newItem.rectTransform = rectTransform;

            if (!newItem.overrideFontSize)
            {
                newItem.fontSize = FontManager.I.fontSize;
            }

            if (!newItem.overrideTotalHeight)
            {
                newItem.totalHeight = TotalHeightManager.I.totalHeight;
            }

            if (!newItem.overrideButtonHeight)
            {
                newItem.buttonHeight = defaultButtonProperties.height;
            }

            if (!newItem.overrideButtonWidth)
            {
                newItem.buttonWidth = defaultButtonProperties.width;
            }

            if (!newItem.overrideFontSize)
            {
                newItem.fontSize = defaultButtonProperties.fontSize;
            }
            
            buttonItemsList.AddItem(newItem);

            FontManager.SetText(itemName, buttonText);

            if (itemName == BackString)
            {
                AssignBackButtonScript(newItem);
            }
            else
            {
                CreateMenuScript.I.Create(itemName);
            }

            return newItem;

        }

        private static GameObject CreateButtonObject(GameObject prefabType, string buttonName, 
                                                     Transform canvasMenuParent)
        {
            var obj = Instantiate(prefabType, canvasMenuParent);
            obj.name = buttonName.Replace(" ", "");
            return obj;
        }

        private void AssignBackButtonScript(MenuListItem newItem)
        {
            foreach (var item in MenuLogicManager.I.menusDictionary
                                            .Where(item => item.menuName == gameObject.name)) 
            {
                var script = item.menuItem;
                newItem.menuItem = script;
                break;
            }
        }
        
        public static void ChangeButtonWidth(MenuListItem item, int buttonWidth)
        {
            item.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonWidth);
        }

        public static void ChangeButtonHeight(MenuListItem item, int buttonHeight)
        {
            item.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonHeight);
        }
    }
}