using System.Linq;
using EraSoren._Core.Helpers;
using EraSoren.Menu.General;
using EraSoren.Menu.ItemTypes;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace EraSoren.Menu.Managers
{
    public class FontManager : Singleton<FontManager>
    {
        [OnValueChanged(nameof(ChangeFontType))]
        public TMP_FontAsset fontType;
        
        [OnValueChanged(nameof(OnFontSizeChange))]
        public float fontSize = 42;

        public void ChangeFontType()
        {
            foreach (var item in MenuLogicManager.I.menuItemCreators
                         .SelectMany(menuItemCreator => menuItemCreator.currentItems
                         .Where(item => !item.overrideFontSize)))
            {
                item.textComponent.font = fontType;
            }
        }

        public void OnFontSizeChange()
        {
            foreach (var item in MenuLogicManager.I.menuItemCreators
                         .SelectMany(menuItemCreator => menuItemCreator.currentItems
                             .Where(item => !item.overrideFontSize)))
            {
                var manager = ItemTypeManagers.I.FindTypeClass(item.itemType);
                if (manager.defaultProperties.overrideFontSizes) continue;
                
                item.fontSize = fontSize;
                ChangeFontSize(item, fontSize);
            }
        }

        public void ChangeFontSize(MenuItem item, float size)
        {
            item.textComponent.fontSize = size;
        }
        
        public static void SetText(string buttonName, TMP_Text itemText)
        {
            itemText.text = buttonName.ToUpper().Replace("İ", "I");
        }
    }
}