﻿using System.Linq;
using EraSoren._Core.Helpers;
using EraSoren.Menu.ItemLists;
using EraSoren.Menu.ItemTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EraSoren.Menu.Managers
{
    public class ButtonManager : Singleton<ButtonManager>
    {
        [Header("Objects")]
        [SerializeField] private GameObject buttonPrefabDefault;
        [SerializeField] private GameObject buttonPrefabBack;
        
        [Header("References")] 
        public DefaultButtonProperties defaultButtonProperties;
        public ButtonItemsList buttonItemsList;

        public const string BackString = "Back";

        public MenuItem FinalizeItem(GameObject obj, string itemName, Transform canvasMenuParent)
        {
            var buttonObj = CreateButtonObject(itemName == BackString ?
                                             buttonPrefabBack : buttonPrefabDefault, itemName, canvasMenuParent);
            
            var newItem = obj.GetComponent<ButtonItem>();
            
            newItem.itemName = itemName;
            newItem.canvasObject = buttonObj;
            newItem.canvasObject.GetComponent<ButtonInteract>().buttonItem = newItem;
            newItem.buttonComponent = buttonObj.GetComponent<Button>();
            newItem.rectTransform = buttonObj.GetComponent<RectTransform>();
            newItem.itemTextComponent = buttonObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            newItem.itemTextComponent.font = FontManager.I.fontType;

            if (!newItem.overrideFontSize)
                newItem.fontSize = FontManager.I.fontSize;

            if (!newItem.overrideTotalHeight)
                newItem.totalHeight = TotalHeightManager.I.totalHeight;

            if (!newItem.overrideButtonHeight)
                newItem.buttonHeight = defaultButtonProperties.height;

            if (!newItem.overrideButtonWidth)
                newItem.buttonWidth = defaultButtonProperties.width;

            if (!newItem.overrideFontSize)
                newItem.fontSize = defaultButtonProperties.fontSize;
            
            buttonItemsList.AddItem(newItem);

            FontManager.SetText(itemName, newItem.itemTextComponent);

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
    }
}