using EraSoren.Menu.General;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Toggle
{
    public class ToggleItem : MenuItem
    {
        protected ToggleItem()
        {
            ItemType = MenuItemTypes.Toggle;
        }
        
        [OnValueChanged(nameof(OnOverrideLabelWidthChange))]
        [TabGroup("Properties")] public bool overrideLabelWidth;
        
        [ShowIf(nameof(overrideLabelWidth))] 
        [OnValueChanged(nameof(OnLabelWidthChange))]
        [TabGroup("Properties")] public int labelWidth;

        private ToggleManager _toggleManager;
        private ToggleManager ToggleManager
        {
            get
            {
                if (_toggleManager == null)
                {
                    _toggleManager = ItemTypeManagers.I.GetManager<ToggleManager>();
                }

                return _toggleManager;
            }
        }
        
        private void OnLabelWidthChange()
        {
            ToggleManager.ChangeLabelWidth(this, labelWidth);
        }

        private void OnOverrideLabelWidthChange()
        {
            labelWidth = ToggleManager.defaultToggleProperties.labelWidth;
            OnLabelWidthChange();
        }

        public virtual void Interact(bool toggle)
        {
            
        }

        public override void AdjustItem()
        {
            base.AdjustItem();
            OnLabelWidthChange();
        }

        protected override void OnDestroy()
        {
            if (ToggleManager == null) return;
            ToggleManager.DestroyToggleItem(this);
            base.OnDestroy();
        }
    }
}