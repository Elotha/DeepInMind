using System.Collections.Generic;
using System.Linq;
using EraSoren._Core.Helpers.Extensions;
using EraSoren.Menu.General;
using EraSoren.Menu.ItemTypes.Button;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.MultipleChoice
{
    public class OptionCreator : MenuItemCreator
    {
        public MultipleChoiceItem multipleChoiceItem;

        protected override void CreateScriptsForNewItems()
        {
            var atLeastOneScriptCreated = false;
            foreach (var item in newItems.Where(item => 
                         !IsThereAnItemWithSameName(item.itemName)))
            {
                var itemType = ItemTypeManagers.I.FindTypeClass(item.itemType);
                var isScriptCreated = itemType.CreateScript(multipleChoiceItem.itemName + item.itemName);
                if (isScriptCreated)
                {
                    atLeastOneScriptCreated = true;
                }
            }
            
            DecideWhetherToRefresh(atLeastOneScriptCreated);
        }

        public override void ContinueAfterRefreshingAssets()
        {
            CreateObjectsForNewItems();
            AddScriptsToNewItems();
            FinalizeItems();
        }

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
                
                var scriptName = multipleChoiceItem.itemName.Replace(" ", "") +
                    child.name.Replace(" ", "") + MenuManager.I.menuNameSuffix;
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

        protected override void FinalizeItems()
        {
            var itemType = ItemTypeManagers.I.FindTypeClass(MenuItemTypes.ChoiceOption);
            if (itemType is ChoiceOptionManager choiceOptionManager)
            {
                foreach (var item in newItems)
                {
                    if (IsThereAnItemWithSameName(item.itemName)) continue;
                
                    var obj = transform.Find(item.itemName).gameObject;
                    var newItem = choiceOptionManager.Finalize(obj, item.itemName, multipleChoiceItem.transform);
                    currentItems.Add(newItem);
                }
                AdjustItems();
                Debug.Log("Items are finalized!");
            }
        }
        protected override bool IsThereAnyEmptyName()
        {
            if (newItems.All(item => item.itemName.Replace(" ", "") != "")) return false;
            Debug.LogError("Item names cannot be empty!");
            return true;
        }

        protected override bool IsThereAnItemWithSameName(string optionName)
        {
            return multipleChoiceItem.options.Any(currentItem => currentItem.itemName == optionName);
        }
    }
}