using System;
using System.Collections.Generic;
using System.Linq;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.General
{
    public abstract class DefaultProperties : MonoBehaviour
    {
        [HideInInspector] public MenuItemTypeManager menuItemTypeManager;
        
        [OnValueChanged(nameof(OnOverrideFontSizeChange))]
        public bool overrideFontSizes;
        
        [ShowIf(nameof(overrideFontSizes))]
        [OnValueChanged(nameof(OnFontSizeChange))]
        public float fontSize = 42f;

        private void Awake()
        {
            menuItemTypeManager = GetComponent<MenuItemTypeManager>();
        }

        protected void OnOverrideFontSizeChange()
        {
            fontSize = FontManager.I.fontSize;
            OnFontSizeChange();
        }

        protected void OnFontSizeChange()
        {
            var items = menuItemTypeManager.itemsList.allItems.Where(x => !x.overrideFontSize);
            foreach (var item in items)
            {
                item.fontSize = fontSize;
                item.OnFontSizeChange();
            }
        }
    }
}