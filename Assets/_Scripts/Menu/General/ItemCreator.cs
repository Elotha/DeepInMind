using System.Collections.Generic;
using System.Linq;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace EraSoren.Menu.General
{
    public abstract class ItemCreator : MonoBehaviour
    {
        [Space(20)]
        public List<MenuItem> currentItems = new ();

        [Space(20)]
        [SerializeField] protected List<MenuItemNameAndType> newItems = new ();

        [Button]
        protected virtual void CreateNewItems()
        {
            if (newItems.Count == 0)
            {
                Debug.LogError("New Items list is empty!");
                return;
            }
            
            if (IsThereAnyEmptyName())
            {
                Debug.LogError("Names cannot be empty!");
                return;
            }
            
            CreateScriptsForNewItems();
        }
        protected virtual void CreateScriptsForNewItems()
        {
            var atLeastOneScriptCreated = false;
            foreach (var item in newItems)
            {
                if (IsThereAnItemWithSameName(item.itemName)) continue;

                var itemType = ItemTypeManagers.I.FindTypeClass(item.itemType);
                var isScriptCreated = itemType.CreateScript(item.itemName);
                if (isScriptCreated)
                {
                    atLeastOneScriptCreated = true;
                }
            }
            DecideWhetherToRefresh(atLeastOneScriptCreated);
        }

        protected virtual void DecideWhetherToRefresh(bool atLeastOneScriptCreated)
        {
            if (atLeastOneScriptCreated)
            {
                ReloadScripts.I.Set(this);
                Debug.Log("The provided scripts are created.");
                AssetDatabase.Refresh();
            }
            else
            {
                Debug.Log("The provided scripts already exist.");
                ContinueAfterRefreshingAssets();
            }
        }

        public virtual void ContinueAfterRefreshingAssets()
        {
            CreateObjectsForNewItems();
            AddScriptsToNewItems();
            FinalizeItems();
        }

        protected virtual void CreateObjectsForNewItems()
        {
            foreach (var item in newItems)
            {
                if (IsThereAnItemWithSameName(item.itemName)) continue;
                
                var itemType = ItemTypeManagers.I.FindTypeClass(item.itemType);
                itemType.CreateObjects(item.itemName, transform);
            }
            
            Debug.Log("New objects are created!");
        }
        protected virtual void AddScriptsToNewItems() { }
        protected virtual void FinalizeItems() { }

        protected virtual bool IsThereAnItemWithSameName(string itemName)
        {
            return currentItems.Any(currentItem => currentItem.itemName == itemName);
        }

        protected virtual bool IsThereAnyEmptyName()
        {
            if (newItems.All(item => item.itemName.Replace(" ", "") != "")) return false;
            Debug.LogError("Item names cannot be empty!");
            return true;
        }

        [Button]
        protected virtual void ClearSubItems()
        {
            for (var i = transform.childCount; i > 0; i--)
            {
                var childObj = transform.GetChild(i - 1).gameObject;
                DestroyImmediate(childObj);
            }
            
            if (DeleteObsoleteMenuItems.I.deleteIfObsolete)
            {
                AssetDatabase.Refresh();
            }
            
            currentItems.Clear();
        }

        [Button]
        private void ClearNewItemsList()
        {
            newItems.Clear();
        }

    }
}