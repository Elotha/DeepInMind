using EraSoren.Menu.ItemTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EraSoren.Menu.Managers
{
    public class ToggleManager : MenuItemTypeManager
    {
        [Header("Objects")]
        [SerializeField] private GameObject togglePrefab;

        [Header("Toggle Properties")]
        public int labelWidth;
        // public MenuItem Finalize(string itemName, Transform canvasMenuParent, Transform parentObject)
        // {
        //     var obj = CreateToggleObject(togglePrefab, itemName, canvasMenuParent);
        //     var toggleComponent = obj.GetComponent<Toggle>();
        //     var rectTransform = obj.transform.GetComponent<RectTransform>();
        //     var itemText = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //
        //     var newItem = new ToggleItem
        //     {
        //         toggleComponent = toggleComponent,
        //         itemTextComponent = itemText,
        //         itemName = itemName
        //     };
        //
        //     newItem.itemTextComponent.font = FontManager.I.fontType;
        //     newItem.rectTransform = rectTransform.GetChild(0).GetComponent<RectTransform>();
        //
        //     if (!newItem.overrideFontSize)
        //     {
        //         newItem.fontSize = FontManager.I.fontSize;
        //     }
        //
        //     if (!newItem.overrideTotalHeight)
        //     {
        //         newItem.totalHeight = TotalHeightManager.I.totalHeight;
        //     }
        //         
        //     FontManager.SetText(itemName, itemText);
        //     CreateMenuScript.I.Create(itemName);
        //     return newItem;
        // }

        private static GameObject CreateToggleObject(GameObject prefabType, string buttonName, 
                                                     Transform canvasMenuParent)
        {
            var obj = Instantiate(prefabType, canvasMenuParent);
            obj.name = buttonName.Replace(" ", "");;
            return obj;
        }

        public static void ChangeLabelWidth(MenuItem item, int labelWidth)
        {
            item.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, labelWidth);
        }

        public override MenuItem Finalize(GameObject obj, string itemName, Transform canvasMenuParent)
        {
            return new StandardButtonItem();
        }

        public override void CreateScript(string itemName)
        {
            
        }

        public override void CreateObjects(string itemName, Transform parentObject)
        {
            
        }
    }
}