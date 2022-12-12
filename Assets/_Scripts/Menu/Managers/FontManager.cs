using System.Linq;
using EraSoren._Core.Helpers;
using EraSoren.Menu.ItemTypes;
using Sirenix.OdinInspector;
using TMPro;

namespace EraSoren.Menu.Managers
{
    public class FontManager : Singleton<FontManager>
    {
        [OnValueChanged("ChangeFontType")]
        public TMP_FontAsset fontType;
        
        [OnValueChanged("OnFontSizeChange")]
        public float fontSize;

        public void ChangeFontType()
        {
            // foreach (var logicObject in MenuLogicManager.I.menuLogicObjects)
            // {
            //     foreach (var item in logicObject.menuItemCreator.currentItems
            //                                     .Where(item => !item.overrideFontSize))
            //     {
            //         item.itemTextComponent.font = fontType;
            //     }
            // }
        }

        public void OnFontSizeChange()
        {
            // foreach (var logicObject in MenuLogicManager.I.menuLogicObjects)
            // {
            //     foreach (var item in logicObject.menuItemCreator.currentItems
            //                                     .Where(item => !item.overrideFontSize))
            //     {
            //         item.fontSize = fontSize;
            //         ChangeFontSize(item, fontSize);
            //     }
            // }

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