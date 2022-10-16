using EraSoren._Core.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EraSoren.Menu
{
    public class ToggleManager : Singleton<ToggleManager>
    {
        [Header("Objects")]
        [SerializeField] private GameObject togglePrefab;

        [Header("Toggle Properties")]
        public int labelWidth;
        public void CreateAToggle(MenuListItem newItem, string itemName, Transform canvasMenuParent)
        {
            var obj = CreateToggleObject(togglePrefab, itemName, canvasMenuParent);
            var toggleComponent = obj.GetComponent<Toggle>();
            var rectTransform = obj.transform.GetComponent<RectTransform>();
            var itemText = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            if (newItem is ToggleListItem toggleItem)
            {
                toggleItem.toggleComponent = toggleComponent;
                newItem.itemTextComponent = itemText;
                newItem.itemTextComponent.font = FontManager.I.fontType;
                newItem.rectTransform = rectTransform.GetChild(0).GetComponent<RectTransform>();

                if (!newItem.overrideFontSize)
                {
                    newItem.fontSize = FontManager.I.fontSize;
                }

                if (!newItem.overrideTotalHeight)
                {
                    newItem.totalHeight = TotalHeightManager.I.totalHeight;
                }
            }
            FontManager.SetText(itemName, itemText);
            CreateMenuScript.I.Create(itemName);
        }

        private static GameObject CreateToggleObject(GameObject prefabType, string buttonName, 
                                                     Transform canvasMenuParent)
        {
            var obj = Instantiate(prefabType, canvasMenuParent);
            obj.name = buttonName.Replace(" ", "");;
            return obj;
        }

        public static void ChangeLabelWidth(MenuListItem item, int labelWidth)
        {
            item.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, labelWidth);
        }
    }
}