using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.Managers
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

        [SerializeField] private SliderManager sliderManager;
        
        private void OnWidthChange()
        {
            foreach (var item in sliderManager.sliderItemsList.allSliderItems.Where(item => !item.overrideSliderWidth))
            {
                item.sliderWidth = sliderWidth;
                SliderManager.ChangeSliderWidth(item, sliderWidth);
            }
        }
        
        private void OnHeightChange()
        {
            foreach (var item in sliderManager.sliderItemsList.allSliderItems.Where(item => !item.overrideSliderWidth))
            {
                item.sliderHeight = sliderHeight;
                SliderManager.ChangeSliderHeight(item, sliderHeight);
            }
        }
        
        public void OnTotalWidthChange()
        {
            foreach (var item in sliderManager.sliderItemsList.allSliderItems.Where(item => !item.overrideSliderWidth))
            {
                item.totalWidth = totalWidth;
                SliderManager.ChangeTotalWidth(item, totalWidth);
            }
        }
        
        private void OnHandleHeightChange()
        {
            foreach (var item in sliderManager.sliderItemsList.allSliderItems.Where(item => !item.overrideSliderWidth))
            {
                item.handleHeight = handleHeight;
                SliderManager.ChangeHandleHeight(item, handleHeight);
            }
        }
    }
}