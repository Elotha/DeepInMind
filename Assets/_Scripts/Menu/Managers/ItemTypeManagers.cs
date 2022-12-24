using System;
using System.Collections.Generic;
using System.Linq;
using EraSoren._Core.Helpers;
using EraSoren.Menu.General;
using EraSoren.Menu.ItemTypes.Button;
using EraSoren.Menu.ItemTypes.MultipleChoice;
using EraSoren.Menu.ItemTypes.Slider;
using EraSoren.Menu.ItemTypes.Toggle;
using Sirenix.OdinInspector;
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
            public MenuItemTypeManager menuItemTypeManager;

            public ItemType(MenuItemTypes menuItemType, MenuItemTypeManager menuItemTypeManager)
            {
                this.menuItemType = menuItemType;
                this.menuItemTypeManager = menuItemTypeManager;
            }
        }

        public MenuItemTypeManager FindTypeClass(MenuItemTypes type)
        {
            return (from itemType in itemTypes 
                where itemType.menuItemType == type 
                select itemType.menuItemTypeManager.GetComponent<MenuItemTypeManager>()).FirstOrDefault();
        }

        public T GetManager<T>() where T : class
        {
            return itemTypes
                .Where(itemType => itemType.menuItemTypeManager.GetType() == typeof(T))
                .Select(itemType => itemType.menuItemTypeManager as T)
                .FirstOrDefault();
        }
    }
}