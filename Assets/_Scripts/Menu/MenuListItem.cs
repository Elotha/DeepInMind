using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace EraSoren.Menu
{
    [Serializable]
    public class MenuListItem
    {
        [TabGroup("General")] public string itemName;
        [TabGroup("General")] public MenuItemTypes itemType;
        [TabGroup("General")] [MultiLineProperty(3)] public string description;
        [TabGroup("References")] public TextMeshProUGUI itemTextComponent;
        [TabGroup("References")] public RectTransform rectTransform;
        [TabGroup("References")] public MenuItem menuItem;
        
        [OnValueChanged("OnOverrideTotalHeightChange")]
        [TabGroup("General")] public bool overrideTotalHeight;
        [ShowIf("overrideTotalHeight")] [OnValueChanged("OnTotalHeightChange")]
        [TabGroup("General")] public int totalHeight;
        
        [OnValueChanged("OnOverrideFontSizeChange")]
        [TabGroup("Properties")] public bool overrideFontSize;
        [ShowIf("overrideFontSize")] 
        [OnValueChanged("OnFontSizeChange")]
        [TabGroup("Properties")] public float fontSize;

        private void OnTotalHeightChange()
        {
            TotalHeightManager.I.OnTotalHeightChange();
        }

        private void OnOverrideTotalHeightChange()
        {
            totalHeight = TotalHeightManager.I.totalHeight;
            TotalHeightManager.I.OnTotalHeightChange();
        }

        private void OnOverrideFontSizeChange()
        {
            fontSize = FontManager.I.fontSize;
            FontManager.I.ChangeFontSize(this, fontSize);
        }

        private void OnFontSizeChange()
        {
            FontManager.I.ChangeFontSize(this, fontSize);
        }
        
    }
}