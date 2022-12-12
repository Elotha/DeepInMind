using EraSoren.Menu.General;
using EraSoren.Menu.Managers;
using TMPro;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Slider
{
    public class SliderManager : MenuItemTypeManager
    {
        [Header("Objects")]
        [SerializeField] private GameObject sliderCanvasPrefab;
        [SerializeField] private GameObject sliderLogicPrefab;
        
        [Header("References")] 
        public DefaultSliderProperties defaultSliderProperties;
        public SliderItemsList sliderItemsList;

        public override MenuItem Finalize(GameObject obj, string itemName, Transform canvasMenuParent)
        {
            var sliderObj = CreateSliderObject(itemName, canvasMenuParent);
            
            var newItem = obj.GetComponent<SliderItem>();
            newItem.sliderTransform = sliderObj.transform.GetChild(1).GetComponent<RectTransform>();
            newItem.handleTransform = sliderObj.transform.GetChild(1).GetChild(2).GetComponent<RectTransform>();
            newItem.textComponent = sliderObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            newItem.itemName = itemName;
            newItem.textComponent.font = FontManager.I.fontType;
            newItem.rectTransform = sliderObj.transform.GetComponent<RectTransform>();
            newItem.canvasObject = sliderObj;
            newItem.canvasObject.GetComponent<SliderInteract>().sliderItem = newItem;

            if (!newItem.overrideFontSize)
                newItem.fontSize = defaultSliderProperties.fontSize;

            if (!newItem.overrideTotalHeight)
                newItem.totalHeight = TotalHeightManager.I.totalHeight;

            if (!newItem.overrideTotalWidth)
                newItem.totalWidth = defaultSliderProperties.totalWidth;

            if (!newItem.overrideSliderWidth)
                newItem.sliderWidth = defaultSliderProperties.sliderWidth;

            if (!newItem.overrideSliderHeight)
                newItem.sliderHeight = defaultSliderProperties.sliderHeight;

            if (!newItem.overrideHandleHeight)
                newItem.handleHeight = defaultSliderProperties.handleHeight;
            
            sliderItemsList.AddItem(newItem);
        
            FontManager.SetText(itemName, newItem.textComponent);
        
            return newItem;
        }

        private GameObject CreateSliderObject(string itemName, Transform parent)
        {
            var sliderObj = Instantiate(sliderCanvasPrefab, parent);
            sliderObj.name = itemName;
            return sliderObj;
        }

        public override void CreateScript(string itemName)
        {
            CreateMenuScript.I.Create(itemName, nameof(SliderItem), MenuItemTypes.Slider);
        }

        public override void CreateObjects(string itemName, Transform parentObject)
        {
            var menuLogicObject = Instantiate(sliderLogicPrefab, parentObject);
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

        public void DestroySliderItem(SliderItem sliderItem)
        {
            sliderItemsList.RemoveItem(sliderItem);
        }
    }
}