using System;
using EraSoren._Core.Helpers.Extensions;
using EraSoren.Menu.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.Managers
{
    [ExecuteAlways]
    public class MenuItemCreator : ItemCreator
    {
        public Transform canvasMenuParent;
        
        [SerializeField] private bool overrideCanvasOffset;
        [ShowIf(nameof(overrideCanvasOffset))] 
        public Vector2 canvasOffset;

        protected override void AddScriptsToNewItems()
        {
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

        protected override void FinalizeItems()
        {
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
            Debug.Log("Items are finalized!");
        }

        protected virtual void AdjustItems()
        {
            foreach (var item in currentItems)
            {
                item.AdjustItem();
            }
            
            LengthInHierarchyManager.ChangeLengthInHierarchy(currentItems);
        }

        [Button]
        private void MakeThisCanvasActive()
        {
            MenuLogicManager.I.SetActiveCanvas(gameObject);
        }

        [Button]
        protected override void ClearSubItems()
        {
            for (var i = canvasMenuParent.childCount; i > 0; i--)
            {
                var childObj = canvasMenuParent.GetChild(i - 1).gameObject;
                DestroyImmediate(childObj);
            }
            base.ClearSubItems();
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