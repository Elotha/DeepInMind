using System.Collections.Generic;
using System.Linq;
using EraSoren.Menu.General;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Button
{
    public class DefaultButtonProperties : DefaultProperties
    {
        [OnValueChanged(nameof(OnWidthChange))]
        public int width = 560;
        
        [OnValueChanged(nameof(OnHeightChange))]
        public int height = 150;
        
        private void OnWidthChange()
        {
            var allItems = ButtonManager.I.itemsList.allItems;
            foreach (var item in allItems)
            {
                if (item is not ButtonItem buttonItem) continue;
                if (buttonItem.overrideButtonWidth) continue;
                
                buttonItem.buttonWidth = width;
                ButtonManager.ChangeButtonWidth(buttonItem, width);
            }
        }
        
        private void OnHeightChange()
        {
            var allItems = ButtonManager.I.itemsList.allItems;
            foreach (var item in allItems)
            {
                if (item is not ButtonItem buttonItem) continue;
                if (buttonItem.overrideButtonHeight) continue;

                buttonItem.buttonHeight = height;
                ButtonManager.ChangeButtonHeight(buttonItem, height);
            }
        }
    }
}