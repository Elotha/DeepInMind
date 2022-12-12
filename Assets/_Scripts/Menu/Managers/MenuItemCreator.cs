using System;
using System.Collections.Generic;
using System.Linq;
using EraSoren._Core.Helpers.Extensions;
using EraSoren.Menu.ItemTypes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.Managers
{
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
        
        private void Start()
        {
            SetButtonEvents();
        }

        [Button]
        private void CreateScriptsForNewItems()
        {
            foreach (var item in newItems)
            {
                var itemType = FindTypeClass(item.itemType);
                itemType.CreateScript(item.itemName);
            }
            if (newItems.Count != 0)
            {
                Debug.Log("New scripts are created!");
            }
        }

        [Button]
        private void CreateObjectsForNewItems()
        {
            foreach (var item in newItems)
            {
                var itemType = FindTypeClass(item.itemType);
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
            foreach (var child in transform.GetAllChildrenList())
            {
                // if (child.name == ButtonManager.BackString) continue;
                
                var itemInfo = child.GetComponent<MenuItemInfo>();
                if (itemInfo == null)
                {
                    Debug.Log("null");
                    continue;
                }
                if (itemInfo.isScriptAdded) continue;
                
                var scriptName = child.name.Replace(" ", "") + MenuManager.I.menuNameSuffix;
                var component = CreateMenuScript.I.AddMenuComponent(scriptName, child.gameObject);

                if (component != null)
                {
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
            foreach (var item in newItems)
            {
                var itemType = FindTypeClass(item.itemType);
                var obj = transform.Find(item.itemName).gameObject;
                var newItem = itemType.Finalize(obj, item.itemName, canvasMenuParent);
                currentItems.Add(newItem);
            }
            newItems.Clear();
            AdjustItems();
            MakeThisCanvasActive();
            if (newItems.Count != 0)
            {
                Debug.Log("Items are finalized!");
            }
        }

        private MenuItemTypeManager FindTypeClass(MenuItemTypes type)
        {
            return ItemTypeManagers.I.itemTypes
                .Where(itemType => itemType.menuItemType == type)
                .Select(itemType => itemType.menuItemTypeManager)
                .FirstOrDefault();
        }

        // Set events yapılacak
        private void SetButtonEvents()
        {
            foreach (var item in currentItems)
            {
                // switch (item.itemType)
                // {
                //     case MenuItemTypes.StandardButton:
                //         if (item.itemName != "Back")
                //         {
                //             Debug.Log("interact");
                //             item.AddListener(item.menuItem.Interact);
                //         }
                //         else
                //         {
                //             Debug.Log("back");
                //             Debug.Log(item.menuItem != null);
                //             item.AddListener(item.menuItem.Back);
                //         }
                //         
                //         // if (item is EnumListItem)
                //         //     Debug.Log("enum");
                //         //
                //         // if (item is SliderListItem)
                //         //     Debug.Log("slider");
                //         //
                //         // if (item is ToggleListItem)
                //         //     Debug.Log("toggle");
                //         //
                //         // if (item is ButtonListItem buttonItem)
                //         // {
                //         //     Debug.Log("button");
                //         //     if (item.itemName != "Back")
                //         //     {
                //         //         buttonItem.buttonComponent.onClick.AddListener(item.menuItem.Interact);
                //         //         Debug.Log("interact");
                //         //     }
                //         //     else
                //         //     {
                //         //         buttonItem.buttonComponent.onClick.AddListener(item.menuItem.Back);
                //         //     }
                //         // }
                //
                //         break;
                //     
                //     case MenuItemTypes.Toggle:
                //         if (item is ToggleListItem toggleItem)
                //         {
                //             toggleItem.toggleComponent.onValueChanged.AddListener(item.menuItem.Interact);
                //         }
                //
                //         break;
                //     
                //     case MenuItemTypes.Slider:     
                //         break;
                //     
                //     case MenuItemTypes.Enum:
                //         
                //         break;
                //     
                //     case MenuItemTypes.InputField: 
                //         break;
                //     
                //     default:                       
                //         throw new ArgumentOutOfRangeException();
                // }
            }
        }

        private void AdjustItems()
        {
            foreach (var item in currentItems)
            {
                item.AdjustItem();
            }
            
            TotalHeightManager.ChangeTotalHeight(currentItems);
        }

        public void DetermineActiveCanvas(GameObject logicObject)
        {
            canvasMenuParent.gameObject.SetActive(logicObject == gameObject);
        }

        [Button]
        private void MakeThisCanvasActive()
        {
            MenuLogicManager.I.SetActiveCanvas(gameObject);
        }
        
        // public IEnumerable<Type> GetFilteredTypeList()
        // {
        //     var q = typeof(MenuListItem).Assembly.GetTypes()
        //                              .Where(x => !x.IsAbstract)  
        //                              .Where(x => !x.IsGenericTypeDefinition) 
        //                              .Where(x => typeof(MenuListItem).IsAssignableFrom(x));
        //
        //     return q;
        // }
        
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