using EraSoren.Menu.General;
using EraSoren.Menu.Managers;
using TMPro;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.MultipleChoice
{
    public class MultipleChoiceManager : MenuItemTypeManager
    {
        [Header("Objects")]
        [SerializeField] private GameObject multipleChoiceCanvasPrefab;
        [SerializeField] private GameObject multipleChoiceLogicPrefab;
        
        [Header("References")] 
        public DefaultMultipleChoiceProperties defaultMultipleChoiceProperties;

        public override MenuItem Finalize(GameObject obj, string itemName, Transform canvasMenuParent)
        {
            var multipleChoiceObj = CreateMultipleChoiceObject(itemName, canvasMenuParent);
            
            var newItem = obj.GetComponent<MultipleChoiceItem>();
            // newItem.sliderTransform = sliderObj.transform.GetChild(1).GetComponent<RectTransform>();
            // newItem.handleTransform = sliderObj.transform.GetChild(1).GetChild(2).GetComponent<RectTransform>();
            newItem.textComponent = multipleChoiceObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            newItem.itemName = itemName;
            newItem.textComponent.font = FontManager.I.fontType;
            newItem.rectTransform = multipleChoiceObj.transform.GetComponent<RectTransform>();
            newItem.canvasObject = multipleChoiceObj;
            // newItem.canvasObject.GetComponent<SliderInteract>().sliderItem = newItem;

            if (!newItem.overrideFontSize)
                newItem.fontSize = defaultMultipleChoiceProperties.fontSize;

            if (!newItem.overrideTotalHeight)
                newItem.totalHeight = TotalHeightManager.I.totalHeight;

            // if (!newItem.overrideTotalWidth)
            //     newItem.totalWidth = defaultMultipleChoiceProperties.totalWidth;
            //
            // if (!newItem.overrideSliderWidth)
            //     newItem.sliderWidth = defaultMultipleChoiceProperties.sliderWidth;
            //
            // if (!newItem.overrideSliderHeight)
            //     newItem.sliderHeight = defaultMultipleChoiceProperties.sliderHeight;
            //
            // if (!newItem.overrideHandleHeight)
            //     newItem.handleHeight = defaultMultipleChoiceProperties.handleHeight;
            
            itemsList.AddItem(newItem);
        
            FontManager.SetText(itemName, newItem.textComponent);
        
            return newItem;
        }

        private GameObject CreateMultipleChoiceObject(string itemName, Transform parent)
        {
            var sliderObj = Instantiate(multipleChoiceCanvasPrefab, parent);
            sliderObj.name = itemName;
            return sliderObj;
        }

        public override void CreateScript(string itemName)
        {
            CreateMenuScript.I.Create(itemName, nameof(MultipleChoiceItem), MenuItemTypes.MultipleChoice);
        }

        public override void CreateObjects(string itemName, Transform parentObject)
        {
            var menuLogicObject = Instantiate(multipleChoiceLogicPrefab, parentObject);
            menuLogicObject.name = itemName;
        }

        // public static void ChangeSliderWidth(MenuItem item, int sliderWidth)
        // {
        //     if (item is MultipleChoiceItem sliderItem)
        //     {
        //         sliderItem.sliderTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sliderWidth);
        //     }
        // }
        
        // public static void ChangeSliderHeight(MenuItem item, int sliderHeight)
        // {
        //     if (item is MultipleChoiceItem sliderItem)
        //     {
        //         sliderItem.sliderTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sliderHeight);
        //     }
        // }

        public static void ChangeTotalWidth(MenuItem item, int totalWidth)
        {
            item.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, totalWidth);
        }

        // public static void ChangeHandleHeight(MenuItem item, int handleHeight)
        // {
        //     if (item is MultipleChoiceItem sliderItem)
        //     {
        //         sliderItem.handleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, handleHeight);
        //     }
        // }

        public void DestroyMultipleChoiceItem(MultipleChoiceItem multipleChoiceItem)
        {
            itemsList.RemoveItem(multipleChoiceItem);
        }
    }
}