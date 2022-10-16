using System.Linq;
using EraSoren._Core.Helpers;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EraSoren.Menu
{
    public class SliderManager : Singleton<SliderManager>
    {
        [Header("Objects")]
        [SerializeField] private GameObject sliderPrefabDefault;

        [Header("General Properties")]
        [OnValueChanged(nameof(OnTotalWidthChange))]
        public int totalWidth = 200;

        [Header("Slider Properties")]
        [OnValueChanged(nameof(OnSliderWidthChange))]
        public int sliderWidth = 560;
        [OnValueChanged(nameof(OnSliderHeightChange))]
        public int sliderHeight = 150;
        [OnValueChanged(nameof(OnHandleHeightChange))]
        public int handleHeight = 60;

        public void CreateASlider(MenuListItem newItem, string itemName, Transform canvasMenuParent)
        {
            var obj = CreateSliderObject(sliderPrefabDefault, itemName, canvasMenuParent);
            var sliderComponent = obj.GetComponent<Slider>();
            var rectTransform = obj.transform.GetComponent<RectTransform>();
            var itemText = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            if (newItem is SliderListItem sliderItem)
            {
                var sliderTransform = obj.transform.GetChild(1).GetComponent<RectTransform>();
                var handleTransform = obj.transform.GetChild(1).GetChild(2).GetComponent<RectTransform>();
                
                sliderItem.sliderComponent = sliderComponent;
                sliderItem.sliderTransform = sliderTransform;
                sliderItem.handleTransform = handleTransform;
                
                newItem.itemTextComponent = itemText;
                newItem.itemTextComponent.font = FontManager.I.fontType;
                newItem.rectTransform = rectTransform;

                if (!newItem.overrideFontSize)
                {
                    newItem.fontSize = FontManager.I.fontSize;
                }

                if (!newItem.overrideTotalHeight)
                {
                    newItem.totalHeight = TotalHeightManager.I.totalHeight;
                }
                
                ChangeTotalWidth(newItem, totalWidth);
                ChangeSliderWidth(newItem, sliderWidth);
                ChangeSliderHeight(newItem, sliderHeight);
                ChangeHandleHeight(newItem, handleHeight);
            }

            FontManager.SetText(itemName, itemText);
            CreateMenuScript.I.Create(itemName);
        }
        
        private static GameObject CreateSliderObject(GameObject prefabType, string buttonName, 
                                                     Transform canvasMenuParent)
        {
            var obj = Instantiate(prefabType, canvasMenuParent);
            obj.name = buttonName.Replace(" ", "").Replace(":", "");
            return obj;
        }

        public static void ChangeSliderWidth(MenuListItem item, int sliderWidth)
        {
            if (item is SliderListItem sliderItem)
            {
                sliderItem.sliderTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sliderWidth);
            }
        }
        
        private void OnSliderWidthChange()
        {
            foreach (var item in from logicObject in MenuLogicManager.I.menuLogicObjects 
                                 from item in logicObject.menuItemCreator.currentItems 
                                 where item.itemType == MenuItemTypes.Slider
                                 select item)
            {
                if (item is SliderListItem sliderItem)
                {
                    if (sliderItem.overrideSliderWidth) continue;
                    sliderItem.sliderWidth = sliderWidth;
                    ChangeSliderWidth(item, sliderWidth);
                }
            }
        }
        
        private void OnSliderHeightChange()
        {
            foreach (var item in from logicObject in MenuLogicManager.I.menuLogicObjects 
                                 from item in logicObject.menuItemCreator.currentItems 
                                 where item.itemType == MenuItemTypes.Slider
                                 select item)
            {
                if (item is SliderListItem sliderItem)
                {
                    if (sliderItem.overrideSliderHeight) continue;
                    sliderItem.sliderHeight = sliderHeight;
                    ChangeSliderHeight(item, sliderHeight);
                }
            }
        }

        public static void ChangeSliderHeight(MenuListItem item, int sliderHeight)
        {
            if (item is SliderListItem sliderItem)
            {
                sliderItem.sliderTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sliderHeight);
            }
        }
        
        public void OnTotalWidthChange()
        {
            foreach (var item in from logicObject in MenuLogicManager.I.menuLogicObjects 
                                 from item in logicObject.menuItemCreator.currentItems 
                                 where item.itemType == MenuItemTypes.Slider
                                 select item)
            {
                if (item is SliderListItem sliderItem)
                {
                    if (sliderItem.overrideTotalWidth) continue;
                    sliderItem.totalWidth = totalWidth;
                    ChangeTotalWidth(item, totalWidth);
                }
            }
        }

        public static void ChangeTotalWidth(MenuListItem item, int totalWidth)
        {
            item.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, totalWidth);
        }
        
        private void OnHandleHeightChange()
        {
            foreach (var item in from logicObject in MenuLogicManager.I.menuLogicObjects 
                                 from item in logicObject.menuItemCreator.currentItems 
                                 where item.itemType == MenuItemTypes.Slider
                                 select item)
            {
                if (item is SliderListItem sliderItem)
                {
                    if (sliderItem.overrideHandleHeight) continue;
                    sliderItem.handleHeight = handleHeight;
                    ChangeHandleHeight(item, handleHeight);
                }
            }
        }

        private static void ChangeHandleHeight(MenuListItem item, int handleHeight)
        {
            if (item is SliderListItem sliderItem)
            {
                sliderItem.handleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, handleHeight);
            }
        }
    }
}