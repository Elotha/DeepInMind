using System;
using EraSoren.Menu.General;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Slider
{
    [Serializable]
    public abstract class SliderItem : MenuItem
    {
        protected SliderItem()
        {
            itemType = MenuItemTypes.Slider;
        }
        
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
        
        [OnValueChanged(nameof(OnOverrideHandleHeightChange))]
        [TabGroup("Properties")] public bool overrideHandleHeight;
        
        [ShowIf(nameof(overrideHandleHeight))] 
        [OnValueChanged(nameof(OnHandleHeightChange))]
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
            SliderManager.ChangeLengthInHierarchy(this, totalWidth);
        }

        private void OnOverrideTotalWidthChange()
        {
            totalWidth = SliderManager.defaultSliderProperties.totalWidth;
            OnSliderHeightChange();
        }

        private void OnHandleHeightChange()
        {
            SliderManager.ChangeHandleHeight(this, handleHeight);
        }

        private void OnOverrideHandleHeightChange()
        {
            handleHeight = SliderManager.defaultSliderProperties.handleHeight;
            OnHandleHeightChange();
        }

        public virtual void Interact(float value) 
        { 
            // TODO: Make this abstract
        }

        public override void AdjustItem()
        {
            base.AdjustItem();
            OnTotalWidthChange();
            OnSliderWidthChange();
            OnSliderHeightChange();
            OnHandleHeightChange();
        }

        protected override void OnDestroy()
        {
            if (SliderManager == null) return;
            SliderManager.DestroySliderItem(this);
            base.OnDestroy();
        }
    }
}