using System;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace EraSoren.Menu
{
    public class ButtonListItem : MenuListItem
    {
        [TabGroup("References")] public Button buttonComponent;
        
        [OnValueChanged("OnOverrideButtonWidthChange")]
        [TabGroup("Properties")] public bool overrideButtonWidth = false;
        
        [ShowIf("overrideButtonWidth")] 
        [OnValueChanged("OnButtonWidthChange")]
        [TabGroup("Properties")] public int buttonWidth;
        
        [OnValueChanged("OnOverrideButtonHeightChange")]
        [TabGroup("Properties")] public bool overrideButtonHeight = false;
        
        [ShowIf("overrideButtonHeight")] 
        [OnValueChanged("OnButtonHeightChange")]
        [TabGroup("Properties")] public int buttonHeight;
        
        private void OnButtonWidthChange()
        {
            ButtonManager.ChangeButtonWidth(this, buttonWidth);
        }

        // private void OnOverrideButtonWidthChange()
        // {
        //     buttonWidth = ButtonManager.I.width;
        //     OnButtonWidthChange();
        // }

        private void OnButtonHeightChange()
        {
            ButtonManager.ChangeButtonHeight(this, buttonHeight);
        }

        // private void OnOverrideButtonHeightChange()
        // {
        //     buttonHeight = ButtonManager.I.height;
        //     OnButtonHeightChange();
        // }

        public override void AddListener(UnityEngine.Events.UnityAction action)
        {
            buttonComponent.onClick.AddListener(action);
            buttonComponent.onClick.Invoke();
            Debug.Log("listener");
        }

        public override void AdjustItem()
        {
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            ButtonManager.ChangeButtonWidth(this, buttonWidth);
            ButtonManager.ChangeButtonHeight(this, buttonHeight);
        }
    }
}