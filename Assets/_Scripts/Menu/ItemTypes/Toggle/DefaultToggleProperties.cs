using System.Collections.Generic;
using System.Linq;
using EraSoren.Menu.General;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Toggle
{
    public class DefaultToggleProperties : DefaultProperties
    {
        [OnValueChanged(nameof(OnLabelWidthChange))]
        public int labelWidth;

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
            var items = ToggleManager.itemsList.allItems;
            foreach (var item in items)
            {
                if (item is not ToggleItem toggleItem) continue;
                if (toggleItem.overrideLabelWidth) continue;
                
                toggleItem.labelWidth = labelWidth;
                ToggleManager.ChangeLabelWidth(item, labelWidth);
            }
        }
    }
}