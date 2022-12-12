using System;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes
{
    [ExecuteInEditMode]
    public abstract class MenuItem : MonoBehaviour
    {
        protected MenuItemTypes ItemType;

        [TabGroup("General")] public string itemName;
        [TabGroup("General")] [MultiLineProperty(3)] public string description;
        
        [TabGroup("References")] public GameObject canvasObject;
        [TabGroup("References")] public TextMeshProUGUI textComponent;
        [TabGroup("References")] public RectTransform rectTransform;
        
        [OnValueChanged(nameof(OnOverrideTotalHeightChange))]
        [TabGroup("General")] public bool overrideTotalHeight;
        
        [ShowIf(nameof(overrideTotalHeight))] [OnValueChanged(nameof(OnTotalHeightChange))]
        [TabGroup("General")] public int totalHeight;
        
        [OnValueChanged(nameof(OnOverrideFontSizeChange))]
        [TabGroup("Properties")] public bool overrideFontSize;
        
        [ShowIf(nameof(overrideFontSize))] 
        [OnValueChanged(nameof(OnFontSizeChange))]
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
        
        public virtual void AdjustItem() { }

        public MenuItemTypes GetItemType()
        {
            return ItemType;
        }

        protected virtual void OnDestroy()
        {
            DeleteObsoleteMenuItems.I.DeleteObsoleteFile(itemName);
        }
    }
}