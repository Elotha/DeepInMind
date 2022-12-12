using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace EraSoren.Menu.ItemTypes
{
    public abstract class ButtonItem : MenuItem
    {
        [TabGroup("References")] public Button buttonComponent;
        
        [OnValueChanged(nameof(OnOverrideButtonWidthChange))]
        [TabGroup("Properties")] public bool overrideButtonWidth = false;
        
        [ShowIf("overrideButtonWidth")] 
        [OnValueChanged(nameof(OnButtonWidthChange))]
        [TabGroup("Properties")] public int buttonWidth;
        
        [OnValueChanged(nameof(OnOverrideButtonHeightChange))]
        [TabGroup("Properties")] public bool overrideButtonHeight = false;
        
        [ShowIf(nameof(overrideButtonHeight))] 
        [OnValueChanged(nameof(OnButtonHeightChange))]
        [TabGroup("Properties")] public int buttonHeight;
        
        private void OnButtonWidthChange()
        {
            ButtonManager.ChangeButtonWidth(this, buttonWidth);
        }

        private void OnOverrideButtonWidthChange()
        {
            buttonWidth = ButtonManager.I.defaultButtonProperties.width;
            OnButtonWidthChange();
        }

        private void OnButtonHeightChange()
        {
            ButtonManager.ChangeButtonHeight(this, buttonHeight);
        }

        private void OnOverrideButtonHeightChange()
        {
            buttonHeight = ButtonManager.I.defaultButtonProperties.height;
            OnButtonHeightChange();
        }

        public virtual void Interact() { }

        public override void AdjustItem()
        {
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            ButtonManager.ChangeButtonWidth(this, buttonWidth);
            ButtonManager.ChangeButtonHeight(this, buttonHeight);
        }
    }
}