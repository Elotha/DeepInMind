using System.Collections.Generic;
using System.Linq;
using EraSoren.Menu.General;
using EraSoren.Menu.ItemTypes.Toggle;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Slider
{
    public class DefaultSliderProperties : DefaultProperties
    {
        [OnValueChanged(nameof(OnWidthChange))]
        public int sliderWidth = 560;
        
        [OnValueChanged(nameof(OnHeightChange))]
        public int sliderHeight = 150;

        [OnValueChanged(nameof(OnTotalWidthChange))]
        public int totalWidth = 200;

        [OnValueChanged(nameof(OnHandleHeightChange))]
        public int handleHeight = 60;

        private SliderManager _sliderManager;
        private SliderManager SliderManager
        {
            get
            {
                if (_sliderManager == null)
                {
                    _sliderManager = ItemTypeManagers.I.GetManager<SliderManager>();
                }

                return _sliderManager;
            }
        }
        
        private void OnWidthChange()
        {
            var items = SliderManager.itemsList.allItems;
            foreach (var item in items)
            {
                if (item is not SliderItem sliderItem) continue;
                if (sliderItem.overrideSliderWidth) continue;

                sliderItem.sliderWidth = sliderWidth;
                SliderManager.ChangeSliderWidth(item, sliderWidth);
            }
        }
        
        private void OnHeightChange()
        {
            var items = SliderManager.itemsList.allItems;
            foreach (var item in items)
            {
                if (item is not SliderItem sliderItem) continue;
                if (sliderItem.overrideSliderHeight) continue;
                
                sliderItem.sliderHeight = sliderHeight;
                SliderManager.ChangeSliderHeight(sliderItem, sliderHeight);
            }
        }
        
        public void OnTotalWidthChange()
        {
            var items = SliderManager.itemsList.allItems;
            foreach (var item in items)
            {
                if (item is not SliderItem sliderItem) continue;
                if (sliderItem.overrideTotalWidth) continue;

                sliderItem.totalWidth = totalWidth;
                SliderManager.ChangeTotalWidth(sliderItem, totalWidth);
            }
        }
        
        private void OnHandleHeightChange()
        {
            var items = SliderManager.itemsList.allItems;
            foreach (var item in items)
            {
                if (item is not SliderItem sliderItem) continue;
                if (sliderItem.overrideHandleHeight) continue;

                sliderItem.handleHeight = handleHeight;
                SliderManager.ChangeHandleHeight(sliderItem, handleHeight);
            }
        }
    }
}