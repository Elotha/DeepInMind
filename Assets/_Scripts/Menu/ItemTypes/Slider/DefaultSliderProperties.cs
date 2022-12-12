using System.Linq;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Slider
{
    public class DefaultSliderProperties : MonoBehaviour
    {
        [OnValueChanged(nameof(OnWidthChange))]
        public int sliderWidth = 560;
        
        [OnValueChanged(nameof(OnHeightChange))]
        public int sliderHeight = 150;

        [OnValueChanged(nameof(OnTotalWidthChange))]
        public int totalWidth = 200;

        [OnValueChanged(nameof(OnHandleHeightChange))]
        public int handleHeight = 60;

        public int fontSize = 42;

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
            var list = SliderManager.sliderItemsList.allSliderItems;
            foreach (var item in list.Where(item => !item.overrideSliderWidth))
            {
                item.sliderWidth = sliderWidth;
                SliderManager.ChangeSliderWidth(item, sliderWidth);
            }
        }
        
        private void OnHeightChange()
        {
            var list = SliderManager.sliderItemsList.allSliderItems;
            foreach (var item in list.Where(item => !item.overrideSliderHeight))
            {
                item.sliderHeight = sliderHeight;
                SliderManager.ChangeSliderHeight(item, sliderHeight);
            }
        }
        
        public void OnTotalWidthChange()
        {
            var list = SliderManager.sliderItemsList.allSliderItems;
            foreach (var item in list.Where(item => !item.overrideTotalWidth))
            {
                item.totalWidth = totalWidth;
                SliderManager.ChangeTotalWidth(item, totalWidth);
            }
        }
        
        private void OnHandleHeightChange()
        {
            var list = SliderManager.sliderItemsList.allSliderItems;
            foreach (var item in list.Where(item => !item.overrideHandleHeight))
            {
                item.handleHeight = handleHeight;
                SliderManager.ChangeHandleHeight(item, handleHeight);
            }
        }
    }
}