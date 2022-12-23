using System.Collections.Generic;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.General
{
    public abstract class DefaultProperties : MonoBehaviour
    {
        [OnValueChanged(nameof(OnOverrideFontSizeChange))]
        public bool overrideFontSizes;
        
        [ShowIf(nameof(overrideFontSizes))]
        [OnValueChanged(nameof(OnFontSizeChange))]
        public float fontSize = 42;
        protected void OnOverrideFontSizeChange()
        {
            fontSize = FontManager.I.fontSize;
            OnFontSizeChange();
        }

        protected void OnFontSizeChange()
        {
            var items = GetItems();
            foreach (var item in items)
            {
                item.fontSize = fontSize;
                item.OnFontSizeChange();
            }
        }

        protected abstract List<MenuItem> GetItems();
    }
}