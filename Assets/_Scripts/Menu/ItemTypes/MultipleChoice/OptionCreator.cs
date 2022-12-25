using System.Collections.Generic;
using System.Linq;
using EraSoren._Core.Helpers.Extensions;
using EraSoren.Menu.General;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.MultipleChoice
{
    public class OptionCreator : ItemCreator
    {
        public MultipleChoiceItem multipleChoiceItem;
        
        [SerializeField] private List<string> newItems = new();

        private MultipleChoiceManager _multipleChoiceManager;
        private MultipleChoiceManager MultipleChoiceManager
        {
            get
            {
                if (_multipleChoiceManager == null)
                {
                    _multipleChoiceManager = ItemTypeManagers.I.GetManager<MultipleChoiceManager>();
                }

                return _multipleChoiceManager;
            }
        }
        
        [Button]
        private void CreateNewItems()
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

        private void CreateScriptsForNewItems()
        {
            foreach (var itemName in newItems.Where(optionName => !IsThereAnItemWithSameName(optionName)))
            {
                CreateMenuScript.I.Create(multipleChoiceItem.itemName + itemName, nameof(MultipleChoiceOption), 
                    MenuItemTypes.MultipleChoice);
            }
            
            ReloadScripts.I.Set(this);
            Debug.Log("The provided scripts are created.");
            AssetDatabase.Refresh();
        }

        public override void ContinueAfterRefreshingAssets()
        {
            CreateObjectsForNewItems();
            AddScriptsToNewItems();
            FinalizeItems();
        }

        private void CreateObjectsForNewItems()
        {
            foreach (var optionName in newItems.Where(optionName => !IsThereAnItemWithSameName(optionName)))
            {
                CreateLogicObject(optionName, transform);
            }
            
            Debug.Log("New objects are created!");
        }

        [Button]
        private void AddScriptsToNewItems()
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
                    component.itemName = child.gameObject.name;
                    itemInfo.itemType = component.GetItemType();
                    itemInfo.isScriptAdded = true;
                    multipleChoiceItem.AddOption(component as MultipleChoiceOption);
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
            foreach (var optionName in newItems.Where(optionName => !IsThereAnItemWithSameName(optionName))) 
            {
                
            }
            
            Debug.Log("Items are finalized!");
        }

        private void CreateLogicObject(string itemName, Transform parentObject)
        {
            var menuLogicObject = Instantiate(MultipleChoiceManager.optionLogicPrefab, parentObject);
            menuLogicObject.name = itemName;
        }

        private bool IsThereAnyEmptyName()
        {
            if (newItems.All(item => item.Replace(" ", "") != "")) return false;
            Debug.LogError("Item names cannot be empty!");
            return true;
        }

        private bool IsThereAnItemWithSameName(string optionName)
        {
            return multipleChoiceItem.options.Any(currentItem => currentItem.itemName == optionName);
        }
    }
}