using System;
using System.Collections.Generic;
using System.Linq;
using EraSoren._Core.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.Managers
{
    public class ItemTypeManagers : Singleton<ItemTypeManagers>
    {
        public List<ItemType> itemTypes = new();

        public static readonly List<ItemType> StaticItemTypes = new ();

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

        public void InitializeStaticItemTypes()
        {
            StaticItemTypes.Clear();
            foreach (var itemType in itemTypes)
            {
                StaticItemTypes.Add(itemType);
            }
        }

        public MenuItemTypeManager FindTypeClass(MenuItemTypes type)
        {
            return itemTypes
                .Where(itemType => itemType.menuItemType == type)
                .Select(itemType => itemType.menuItemTypeManager)
                .FirstOrDefault();
        }

        public T GetManager<T>() where T : MenuItemTypeManager
        {
            return itemTypes
                .Where(itemType => itemType.menuItemTypeManager.GetType() == typeof(T))
                .Select(itemType => itemType.menuItemTypeManager as T)
                .FirstOrDefault();
        }
    }
}