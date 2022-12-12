using System.Linq;
using EraSoren._Core.Helpers;
using EraSoren.Menu.ItemLists;
using EraSoren.Menu.ItemTypes;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EraSoren.Menu.Managers
{
    public class SliderManager : MenuItemTypeManager
    {
        [Header("Objects")]
        [SerializeField] private GameObject sliderPrefabDefault;
        [SerializeField] private GameObject standardSliderLogicPrefab;
        
        [Header("References")] 
        public DefaultSliderProperties defaultSliderProperties;
        public SliderItemsList sliderItemsList;

        public override MenuItem Finalize(GameObject obj, string itemName, Transform canvasMenuParent)
        {
            var sliderObj = CreateSliderObject(itemName, canvasMenuParent);
            
            var newItem = obj.GetComponent<SliderItem>();
            newItem.sliderComponent = sliderObj.GetComponent<Slider>();
            newItem.sliderTransform = sliderObj.transform.GetChild(1).GetComponent<RectTransform>();
            newItem.handleTransform = sliderObj.transform.GetChild(1).GetChild(2).GetComponent<RectTransform>();
            newItem.itemTextComponent = sliderObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            newItem.itemName = itemName;
            newItem.itemTextComponent.font = FontManager.I.fontType;
            newItem.rectTransform = sliderObj.transform.GetComponent<RectTransform>();

            if (!newItem.overrideFontSize)
                newItem.fontSize = defaultSliderProperties.fontSize;

            if (!newItem.overrideTotalHeight)
                newItem.totalHeight = TotalHeightManager.I.totalHeight;

            if (!newItem.overrideSliderWidth)
                newItem.sliderWidth = defaultSliderProperties.sliderWidth;

            if (!newItem.overrideSliderHeight)
                newItem.sliderHeight = defaultSliderProperties.sliderHeight;
        
            FontManager.SetText(itemName, newItem.itemTextComponent);
        
            return newItem;
        }

        private GameObject CreateSliderObject(string itemName, Transform parent)
        {
            var sliderObj = Instantiate(sliderPrefabDefault, parent);
            sliderObj.name = itemName;
            return sliderObj;
        }

        public override void CreateScript(string itemName)
        {
            CreateMenuScript.I.Create(itemName, nameof(SliderItem));
        }

        public override void CreateObjects(string itemName, Transform parentObject)
        {
            var menuLogicObject = Instantiate(standardSliderLogicPrefab, parentObject);
            menuLogicObject.name = itemName;
        }

        public static void ChangeSliderWidth(MenuItem item, int sliderWidth)
        {
            if (item is SliderItem sliderItem)
            {
                sliderItem.sliderTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sliderWidth);
            }
        }

        public static void ChangeSliderHeight(MenuItem item, int sliderHeight)
        {
            if (item is SliderItem sliderItem)
            {
                sliderItem.sliderTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sliderHeight);
            }
        }

        public static void ChangeTotalWidth(MenuItem item, int totalWidth)
        {
            item.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, totalWidth);
        }

        public static void ChangeHandleHeight(MenuItem item, int handleHeight)
        {
            if (item is SliderItem sliderItem)
            {
                sliderItem.handleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, handleHeight);
            }
        }
    }
}