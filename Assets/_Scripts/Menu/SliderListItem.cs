using System;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace EraSoren.Menu
{
    [Serializable]
    public class SliderListItem : MenuListItem
    {
        [TabGroup("References")] public Slider sliderComponent;
        [TabGroup("References")] public RectTransform sliderTransform;
        [TabGroup("References")] public RectTransform handleTransform;
        
        [OnValueChanged(nameof(OnOverrideTotalWidthChange))]
        [TabGroup("Properties")] public bool overrideTotalWidth;
        
        [ShowIf("overrideTotalWidth")] 
        [OnValueChanged(nameof(OnTotalWidthChange))]
        [TabGroup("Properties")] public int totalWidth;
        
        [OnValueChanged(nameof(OnOverrideSliderWidthChange))]
        [TabGroup("Properties")] public bool overrideSliderWidth;
        
        [ShowIf("overrideSliderWidth")] 
        [OnValueChanged(nameof(OnSliderWidthChange))]
        [TabGroup("Properties")] public int sliderWidth;
        
        [OnValueChanged(nameof(OnOverrideSliderHeightChange))]
        [TabGroup("Properties")] public bool overrideSliderHeight;
        
        [ShowIf("overrideSliderHeight")] 
        [OnValueChanged(nameof(OnSliderHeightChange))]
        [TabGroup("Properties")] public int sliderHeight;
        
        [OnValueChanged(nameof(OnOverrideSliderHeightChange))]
        [TabGroup("Properties")] public bool overrideHandleHeight;
        
        [ShowIf("overrideSliderHeight")] 
        [OnValueChanged(nameof(OnSliderHeightChange))]
        [TabGroup("Properties")] public int handleHeight;
        
        private void OnSliderWidthChange()
        {
            SliderManager.ChangeSliderWidth(this, sliderWidth);
        }

        private void OnOverrideSliderWidthChange()
        {
            // sliderWidth = SliderManager.I.sliderWidth;
            // OnSliderWidthChange();
        }

        private void OnSliderHeightChange()
        {
            SliderManager.ChangeSliderHeight(this, sliderHeight);
        }

        private void OnOverrideSliderHeightChange()
        {
            // sliderHeight = SliderManager.I.sliderHeight;
            // OnSliderHeightChange();
        }

        private void OnOverrideTotalWidthChange()
        {
            // totalWidth = SliderManager.I.totalWidth;
            // OnSliderHeightChange();
        }

        private void OnTotalWidthChange()
        {
            SliderManager.ChangeTotalWidth(this, totalWidth);
        }
    }
}