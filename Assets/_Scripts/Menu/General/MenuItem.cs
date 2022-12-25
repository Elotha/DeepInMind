using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace EraSoren.Menu.General
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public abstract class MenuItem : MonoBehaviour
    {
        public MenuItemTypes itemType;

        [TabGroup("General")] public string itemName;
        [TabGroup("General")] [MultiLineProperty(3)] public string description;
        
        [TabGroup("References")] public GameObject canvasObject;
        [TabGroup("References")] public TextMeshProUGUI textComponent;
        [TabGroup("References")] public RectTransform rectTransform;
        
        [OnValueChanged(nameof(OnOverrideLengthInHierarchyChange))]
        [TabGroup("General")] public bool overrideLengthInHierarchy;
        
        [ShowIf(nameof(overrideLengthInHierarchy))] 
        [OnValueChanged(nameof(OnLengthInHierarchyChange))]
        [TabGroup("General")] public int lengthInHierarchy;
        
        [OnValueChanged(nameof(OnOverrideFontSizeChange))]
        [TabGroup("Properties")] public bool overrideFontSize;
        
        [ShowIf(nameof(overrideFontSize))] 
        [OnValueChanged(nameof(OnFontSizeChange))]
        [TabGroup("Properties")] public float fontSize;

        private void OnLengthInHierarchyChange()
        {
            LengthInHierarchyManager.I.OnLengthInHierarchyChange();
        }

        private void OnOverrideLengthInHierarchyChange()
        {
            lengthInHierarchy = LengthInHierarchyManager.I.LengthInHierarchy;
            LengthInHierarchyManager.I.OnLengthInHierarchyChange();
        }

        private void OnOverrideFontSizeChange()
        {
            SetFontSize();
        }

        public void SetFontSize()
        {
            var manager = ItemTypeManagers.I.FindTypeClass(itemType);
            fontSize = manager.defaultProperties.overrideFontSizes
                ? manager.defaultProperties.fontSize
                : FontManager.I.fontSize;
            FontManager.I.ChangeFontSize(this, fontSize);
        }

        public void OnFontSizeChange()
        {
            FontManager.I.ChangeFontSize(this, fontSize);
        }

        public virtual void AdjustItem()
        {
            OnLengthInHierarchyChange();
        }

        public MenuItemTypes GetItemType()
        {
            return itemType;
        }

        protected virtual void OnDestroy()
        {
            if (Application.isPlaying) return;
            if (DeleteObsoleteMenuItems.I == null) return;
            DeleteObsoleteMenuItems.I.DeleteObsoleteFile(itemName);
        }
    }
}