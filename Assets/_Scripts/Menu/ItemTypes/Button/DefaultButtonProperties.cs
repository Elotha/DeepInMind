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
            foreach (var buttonItem in ButtonManager.I.buttonItemsList.allButtonItems.Where(buttonItem => !buttonItem.overrideButtonWidth))
            {
                buttonItem.buttonWidth = width;
                ButtonManager.ChangeButtonWidth(buttonItem, width);
            }
        }
        
        private void OnHeightChange()
        {
            foreach (var buttonItem in ButtonManager.I.buttonItemsList.allButtonItems.Where(buttonItem => !buttonItem.overrideButtonHeight))
            {
                buttonItem.buttonHeight = height;
                ButtonManager.ChangeButtonHeight(buttonItem, height);
            }
        }

        protected override List<MenuItem> GetItems()
        {
            var toggleItems = ButtonManager.I.buttonItemsList.allButtonItems
                .Where(item => !item.overrideFontSize);
            var parentList = toggleItems.Cast<MenuItem>().ToList();
            return parentList;
        }
    }
}