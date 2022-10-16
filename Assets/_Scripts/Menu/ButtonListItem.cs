using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace EraSoren.Menu
{
    public class ButtonListItem : MenuListItem
    {
        [TabGroup("References")] public Button buttonComponent;
        
        [OnValueChanged("OnOverrideButtonWidthChange")]
        [TabGroup("Properties")] public bool overrideButtonWidth;
        
        [ShowIf("overrideButtonWidth")] 
        [OnValueChanged("OnButtonWidthChange")]
        [TabGroup("Properties")] public int buttonWidth;
        
        [OnValueChanged("OnOverrideButtonHeightChange")]
        [TabGroup("Properties")] public bool overrideButtonHeight;
        
        [ShowIf("overrideButtonHeight")] 
        [OnValueChanged("OnButtonHeightChange")]
        [TabGroup("Properties")] public int buttonHeight;
        
        private void OnButtonWidthChange()
        {
            ButtonManager.ChangeButtonWidth(this, buttonWidth);
        }

        private void OnOverrideButtonWidthChange()
        {
            buttonWidth = ButtonManager.I.width;
            OnButtonWidthChange();
        }

        private void OnButtonHeightChange()
        {
            ButtonManager.ChangeButtonHeight(this, buttonHeight);
        }

        private void OnOverrideButtonHeightChange()
        {
            buttonHeight = ButtonManager.I.height;
            OnButtonHeightChange();
        }
    }
}