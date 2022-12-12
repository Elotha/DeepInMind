using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.Managers
{
    public class DefaultButtonProperties : MonoBehaviour
    {
        [OnValueChanged(nameof(OnWidthChange))]
        public int width = 560;
        [OnValueChanged(nameof(OnHeightChange))]
        public int height = 150;
        public int fontSize = 42;
        
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
    }
}