using System.Linq;
using EraSoren.Menu.General;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Toggle
{
    public class DefaultToggleProperties : MonoBehaviour
    {
        [OnValueChanged(nameof(OnLabelWidthChange))]
        public int labelWidth;
        
        
        public int fontSize = 42;

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
            var list = ToggleManager.toggleItemsList.allToggleItems;
            foreach (var item in list.Where(item => !item.overrideLabelWidth))
            {
                item.labelWidth = labelWidth;
                ToggleManager.ChangeLabelWidth(item, labelWidth);
            }
        }
    }
}