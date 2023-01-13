using System.Threading.Tasks;
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

        private GameObject CreateSliderObject(string itemName, Transform parent)
        {
            var sliderObj = Instantiate(sliderCanvasPrefab, parent);
            sliderObj.name = itemName;
            return sliderObj;
        }

        public override bool CreateScript(string itemName)
        {
            return CreateMenuScript.I.CreateScript(itemName, nameof(SliderItem), MenuItemTypes.Slider);
        }

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
            newItem.SetFontSize();
            newItem.lengthInHierarchy = LengthInHierarchyManager.I.lengthInHierarchy;
            newItem.totalWidth = defaultSliderProperties.totalWidth;
            newItem.sliderWidth = defaultSliderProperties.sliderWidth;
            newItem.sliderHeight = defaultSliderProperties.sliderHeight;
            newItem.handleHeight = defaultSliderProperties.handleHeight;
        
            FontManager.SetText(itemName, newItem.textComponent);
            
            itemsList.AddItem(newItem);
        
            return newItem;
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

        public static void ChangeHandleHeight(MenuItem item, int handleHeight)
        {
            if (item is SliderItem sliderItem)
            {
                sliderItem.handleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, handleHeight);
            }
        }

        public void DestroySliderItem(SliderItem sliderItem)
        {
            itemsList.RemoveItem(sliderItem);
        }
    }
}