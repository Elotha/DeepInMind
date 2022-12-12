﻿using System;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace EraSoren.Menu.ItemTypes
{
    [Serializable]
    public class SliderItem : MenuItem
    {
        protected SliderItem()
        {
            ItemType = MenuItemTypes.Slider;
        }
        
        [TabGroup("References")] public Slider sliderComponent;
        [TabGroup("References")] public RectTransform sliderTransform;
        [TabGroup("References")] public RectTransform handleTransform;
        
        [OnValueChanged(nameof(OnOverrideTotalWidthChange))]
        [TabGroup("Properties")] public bool overrideTotalWidth;
        
        [ShowIf(nameof(overrideTotalWidth))] 
        [OnValueChanged(nameof(OnTotalWidthChange))]
        [TabGroup("Properties")] public int totalWidth;
        
        [OnValueChanged(nameof(OnOverrideSliderWidthChange))]
        [TabGroup("Properties")] public bool overrideSliderWidth;
        
        [ShowIf(nameof(overrideSliderWidth))] 
        [OnValueChanged(nameof(OnSliderWidthChange))]
        [TabGroup("Properties")] public int sliderWidth;
        
        [OnValueChanged(nameof(OnOverrideSliderHeightChange))]
        [TabGroup("Properties")] public bool overrideSliderHeight;
        
        [ShowIf(nameof(overrideSliderHeight))] 
        [OnValueChanged(nameof(OnSliderHeightChange))]
        [TabGroup("Properties")] public int sliderHeight;
        
        [OnValueChanged(nameof(OnOverrideSliderHeightChange))]
        [TabGroup("Properties")] public bool overrideHandleHeight;
        
        [ShowIf(nameof(overrideSliderHeight))] 
        [OnValueChanged(nameof(OnSliderHeightChange))]
        [TabGroup("Properties")] public int handleHeight;

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
        
        private void OnSliderWidthChange()
        {
            SliderManager.ChangeSliderWidth(this, sliderWidth);
        }

        private void OnOverrideSliderWidthChange()
        {
            sliderWidth = SliderManager.defaultSliderProperties.sliderWidth;
            OnSliderWidthChange();
        }

        private void OnSliderHeightChange()
        {
            SliderManager.ChangeSliderHeight(this, sliderHeight);
        }

        private void OnOverrideSliderHeightChange()
        {
            sliderHeight = SliderManager.defaultSliderProperties.sliderHeight;;
            OnSliderHeightChange();
        }

        private void OnTotalWidthChange()
        {
            SliderManager.ChangeTotalWidth(this, totalWidth);
        }

        private void OnOverrideTotalWidthChange()
        {
            totalWidth = SliderManager.defaultSliderProperties.totalWidth;
            OnSliderHeightChange();
        }
    }
}