using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace EraSoren.Menu.ItemTypes
{
    public class ToggleItem : MenuItem
    {
        [TabGroup("References")] public Toggle toggleComponent;
        
        [OnValueChanged("OnOverrideToggleLabelWidthChange")]
        [TabGroup("Properties")] public bool overrideToggleLabelWidth;
        
        [ShowIf("overrideToggleLabelWidth")] 
        [OnValueChanged("OnToggleLabelWidthChange")]
        [TabGroup("Properties")] public int toggleLabelWidth;
        
        private void OnToggleLabelWidthChange()
        {
            ToggleManager.ChangeLabelWidth(this, toggleLabelWidth);
        }

        private void OnOverrideToggleLabelWidthChange()
        {
            // toggleLabelWidth = ToggleManager.I.labelWidth;
            // OnToggleLabelWidthChange();
        }
    }
}