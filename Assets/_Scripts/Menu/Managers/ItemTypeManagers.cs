using System;
using System.Collections.Generic;
using EraSoren._Core.Helpers;
using UnityEngine;

namespace EraSoren.Menu.Managers
{
    public class ItemTypeManagers : Singleton<ItemTypeManagers>
    {
        public List<ItemType> itemTypes = new();

        [Serializable]
        public struct ItemType
        {
            public MenuItemTypes menuItemType;
            public MenuItemTypeManager MenuItemTypeManager;

            public ItemType(MenuItemTypes menuItemType, MenuItemTypeManager menuItemTypeManager)
            {
                this.menuItemType = menuItemType;
                MenuItemTypeManager = menuItemTypeManager;
            }
        }
    }
}