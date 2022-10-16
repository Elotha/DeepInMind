using System.Linq;
using EraSoren._Core.Helpers;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EraSoren.Menu
{
    public class ButtonManager : Singleton<ButtonManager>
    {
        [Header("Objects")]
        [SerializeField] private GameObject buttonPrefabDefault;
        [SerializeField] private GameObject buttonPrefabBack;

        [Header("Button Properties")]
        [OnValueChanged("OnWidthChange")]
        public int width = 560;
        [OnValueChanged("OnHeightChange")]
        public int height = 150;
        public int fontSize = 42;
        public const string BackString = "Back";

        public void CreateAButton(MenuListItem newItem, string buttonName, Transform canvasMenuParent)
        {
            var obj = CreateButtonObject(buttonName == BackString ?
                                             buttonPrefabBack : buttonPrefabDefault, buttonName, canvasMenuParent);
            var buttonComponent = obj.GetComponent<Button>();
            var rectTransform = obj.GetComponent<RectTransform>();
            var buttonText = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            if (newItem is ButtonListItem buttonİtem)
            {
                buttonİtem.buttonComponent = buttonComponent;
                newItem.itemTextComponent = buttonText;

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

                if (!buttonİtem.overrideButtonHeight)
                {
                    buttonİtem.buttonHeight = height;
                }

                if (!buttonİtem.overrideButtonWidth)
                {
                    buttonİtem.buttonWidth = width;
                }

                if (!newItem.overrideFontSize)
                {
                    newItem.fontSize = fontSize;
                }
            }

            FontManager.SetText(buttonName, buttonText);

            if (buttonName == BackString)
            {
                AssignBackButtonScript(newItem);
            }
            else
            {
                CreateMenuScript.I.Create(buttonName);
            }

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
                                            .Where(item => item.menuName == gameObject.name)) {
                var script = item.menuItem;
                newItem.menuItem = script;
                break;
            }
        }

        private void OnWidthChange()
        {
            foreach (var item in from logicObject in MenuLogicManager.I.menuLogicObjects 
                                 from item in logicObject.menuItemCreator.currentItems 
                                 where item.itemType == MenuItemTypes.Button
                                 select item)
            {
                if (item is ButtonListItem buttonItem)
                {
                    if (buttonItem.overrideButtonWidth) continue;
                    buttonItem.buttonWidth = width;
                    ChangeButtonWidth(buttonItem, width);
                }
            }
        }

        public static void ChangeButtonWidth(MenuListItem item, int buttonWidth)
        {
            item.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonWidth);
        }
        
        private void OnHeightChange()
        {
            foreach (var item in from logicObject in MenuLogicManager.I.menuLogicObjects 
                                 from item in logicObject.menuItemCreator.currentItems 
                                 where item.itemType == MenuItemTypes.Button 
                                 select item)
            {
                if (item is ButtonListItem buttonItem)
                {
                    if (buttonItem.overrideButtonHeight) continue;
                    buttonItem.buttonHeight = height;
                    ChangeButtonHeight(buttonItem, height);
                }
            }
        }

        public static void ChangeButtonHeight(MenuListItem item, int buttonHeight)
        {
            item.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonHeight);
        }
    }
}