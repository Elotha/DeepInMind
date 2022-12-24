using System;
using System.Collections.Generic;
using System.Linq;
using EraSoren._Core.Helpers.Extensions;
using EraSoren.Menu.General;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using MenuItem = EraSoren.Menu.General.MenuItem;

namespace EraSoren.Menu.Managers
{
    [ExecuteAlways]
    public class MenuItemCreator : MonoBehaviour
    {
        public Transform canvasMenuParent;
        
        [SerializeField] private bool overrideCanvasOffset;
        [ShowIf("overrideCanvasOffset")] 
        public Vector2 canvasOffset;
        
        [Space(20)]
        [SerializeReference] public List<MenuItem> currentItems = new ();

        [Space(20)]
        [SerializeField] private List<MenuItemNameAndType> newItems = new ();
        
        [Button]
        private void CreateScriptsForNewItems()
        {
            if (IsThereAnyEmptyName()) return;
            
            foreach (var item in newItems)
            {
                if (IsThereAnItemWithSameName(item.itemName)) continue;
                
                var itemType = ItemTypeManagers.I.FindTypeClass(item.itemType);
                itemType.CreateScript(item.itemName);
            }
            if (newItems.Count != 0)
            {
                Debug.Log("The provided scripts are already created.");
            }
        }

        [Button]
        private void CreateObjectsForNewItems()
        {
            if (IsThereAnyEmptyName()) return;
            
            foreach (var item in newItems)
            {
                if (IsThereAnItemWithSameName(item.itemName)) continue;
                
                var itemType = ItemTypeManagers.I.FindTypeClass(item.itemType);
                itemType.CreateObjects(item.itemName, transform);
            }
            if (newItems.Count != 0)
            {
                Debug.Log("New objects are created!");
            }
        }

        [Button]
        private void AddScriptsToNewItems()
        {
            if (IsThereAnyEmptyName()) return;
            
            foreach (var child in transform.GetAllChildrenList())
            {
                var itemInfo = child.GetComponent<MenuItemInfo>();
                if (itemInfo == null)
                {
                    Debug.LogError("MenuItemInfo script that you are trying to get is null!");
                    continue;
                }
                if (itemInfo.isScriptAdded) continue;
                
                var scriptName = child.name.Replace(" ", "") + MenuManager.I.menuNameSuffix;
                var component = CreateMenuScript.I.AddMenuComponent(scriptName, child.gameObject);

                if (component != null)
                {
                    itemInfo.itemType = component.GetItemType();
                    itemInfo.isScriptAdded = true;
                    continue;
                }
                
                Debug.LogError("The component you have tried to add is null!");
                return;
            }
            Debug.Log("Scripts added!");
        }

        [Button]
        private void FinalizeItems()
        {
            if (IsThereAnyEmptyName()) return;
            
            foreach (var item in newItems)
            {
                if (IsThereAnItemWithSameName(item.itemName)) continue;
                
                var itemType = ItemTypeManagers.I.FindTypeClass(item.itemType);
                var obj = transform.Find(item.itemName).gameObject;
                var newItem = itemType.Finalize(obj, item.itemName, canvasMenuParent);
                currentItems.Add(newItem);
            }
            AdjustItems();
            MakeThisCanvasActive();
            if (newItems.Count != 0)
            {
                Debug.Log("Items are finalized!");
            }
        }

        private bool IsThereAnItemWithSameName(string itemName)
        {
            return currentItems.Any(currentItem => currentItem.itemName == itemName);
        }

        private void AdjustItems()
        {
            foreach (var item in currentItems)
            {
                item.AdjustItem();
            }
            
            TotalHeightManager.ChangeTotalHeight(currentItems);
        }

        private bool IsThereAnyEmptyName()
        {
            if (newItems.All(item => item.itemName.Replace(" ", "") != "")) return false;
            Debug.LogError("Item names cannot be empty!");
            return true;
        }

        [Button]
        private void MakeThisCanvasActive()
        {
            MenuLogicManager.I.SetActiveCanvas(gameObject);
        }

        [Button]
        private void ClearSubItems()
        {
            for (var i = transform.childCount; i > 0; i--)
            {
                var childObj = transform.GetChild(i - 1).gameObject;
                DestroyImmediate(childObj);
            }

            for (var i = canvasMenuParent.childCount; i > 0; i--)
            {
                var childObj = canvasMenuParent.GetChild(i - 1).gameObject;
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

        private void OnDestroy()
        {
            if (MenuLogicManager.I == null) return;
            MenuLogicManager.I.menuItemCreators.Remove(this);
        }
    }

    [Serializable]
    public struct MenuItemNameAndType
    {
        public string itemName;
        public MenuItemTypes itemType;

        public MenuItemNameAndType(string itemName, MenuItemTypes itemType)
        {
            this.itemName = itemName;
            this.itemType = itemType;
        }
    }
}