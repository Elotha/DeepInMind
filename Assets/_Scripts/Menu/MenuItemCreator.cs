using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu
{
    public class MenuItemCreator : MonoBehaviour
    {
        public Transform canvasMenuParent;
        
        [SerializeField] private bool overrideCanvasOffset;
        [ShowIf("overrideCanvasOffset")] 
        public Vector2 canvasOffset;
        
        [Space(20)]
        public List<MenuListItem> currentItems;

        [Space(20)]
        [SerializeField] private MenuItemTypes newItemType;
        [SerializeField] private List<string> newItems = new List<string>();
        
        private void Start()
        {
            SetButtonEvents();
        }

        [Button]
        private void CreateNewItems()
        {
            switch (newItemType)
            {
                case MenuItemTypes.Button:
                    foreach (var itemName in newItems)
                    {
                        var buttonItem = new ButtonListItem();
                        currentItems.Add(buttonItem);
                        AssignItemProperties(buttonItem, itemName);
                        ButtonManager.I.CreateAButton(buttonItem, itemName == ButtonManager.BackString ? 
                                                          ButtonManager.BackString : itemName, canvasMenuParent);
                    }
                    break;
                
                case MenuItemTypes.Toggle:
                    foreach (var itemName in newItems)
                    {
                        var toggleItem = new ToggleListItem();
                        currentItems.Add(toggleItem);
                        AssignItemProperties(toggleItem, itemName);
                        ToggleManager.I.CreateAToggle(toggleItem, itemName, canvasMenuParent);
                    }
                    break;
                
                case MenuItemTypes.Slider: 
                    foreach (var itemName in newItems)
                    {
                        var sliderItem = new SliderListItem();
                        currentItems.Add(sliderItem);
                        AssignItemProperties(sliderItem, itemName);
                        SliderManager.I.CreateASlider(sliderItem, itemName, canvasMenuParent);
                    }
                    break;
                
                case MenuItemTypes.Enum:       
                    break;
                
                case MenuItemTypes.InputField: 
                    break;
                
                default:                       
                    throw new ArgumentOutOfRangeException();
            }
            newItems.Clear();
            AdjustItems();
        }

        private MenuListItem AssignItemProperties(MenuListItem item, string itemName)
        {
            item.itemName = itemName;
            item.itemType = newItemType;
            return item;
        }

        // Set events yapılacak
        private void SetButtonEvents()
        {
            foreach (var item in currentItems)
            {
                switch (item.itemType)
                {
                    case MenuItemTypes.Button:
                        if (item is ButtonListItem buttonItem)
                        {
                            if (item.itemName != "Back")
                            {
                                buttonItem.buttonComponent.onClick.AddListener(item.menuItem.Interact);
                            }
                            else
                            {
                                buttonItem.buttonComponent.onClick.AddListener(item.menuItem.Back);
                            }
                        }

                        break;
                    
                    case MenuItemTypes.Toggle:
                        if (item is ToggleListItem toggleItem)
                        {
                            toggleItem.toggleComponent.onValueChanged.AddListener(item.menuItem.Interact);
                        }

                        break;
                    
                    case MenuItemTypes.Slider:     
                        break;
                    
                    case MenuItemTypes.Enum:
                        
                        break;
                    
                    case MenuItemTypes.InputField: 
                        break;
                    
                    default:                       
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        // private void EliminateAlreadyExistingNames()
        // {
        //     var newItemsCount = newItemNames.Count;
        //     for (var k = 0; k < newItemsCount; k++) 
        //     {
        //         if (currentItems.Any(x => x.itemName == newItemNames[k])) 
        //         {
        //             Debug.Log("Deleted a new button name: " + newItemNames[k]);
        //             newItemNames.Remove(newItemNames[k]);
        //             k--;
        //             newItemsCount--;
        //         }
        //     }
        // }

        [Button("Add Scripts To This Logic Object")]
        private void AddScriptToObject()
        {
            var atLeastOneAdded = false;
            
            // Add every item in menu canvas object to the item list of the menu logic script
            foreach (var item in currentItems.Where(item => item.menuItem == null)) {
                
                atLeastOneAdded = true;

                var str = item.itemName.Replace(" ", "").Replace(":","");
                str += MenuManager.MenuNameSuffix;
                var script = CreateMenuScript.I.AddMenuComponent(str, gameObject);
                MenuLogicManager.I.AddNewMenuDictionary(str, script);

                // Add listeners to every button
                item.menuItem = script;
            }
            
            // Log message
            if (atLeastOneAdded) {
                Debug.Log("Scripts have been added to the menu logic object!");
            }
            else {
                Debug.LogError("There is no new scripts to add!");
            }
        }

        private void AdjustItems()
        {
            // Assign the properties of every item
            foreach (var item in currentItems)
            {
                var rectTransform = item.rectTransform;
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
                
                if (item.itemType == MenuItemTypes.Button)
                {
                    if (item is ButtonListItem buttonItem)
                    {
                        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonItem.buttonWidth);
                        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonItem.buttonHeight);
                    }
                }
            }
            
            TotalHeightManager.ChangeTotalHeight(currentItems);
        }

        [Button]
        private void MakeThisCanvasActive()
        {
            MenuLogicManager.I.SetCanvasActivityOnButton(canvasMenuParent.gameObject);
        }
        
        public IEnumerable<Type> GetFilteredTypeList()
        {
            var q = typeof(MenuListItem).Assembly.GetTypes()
                                     .Where(x => !x.IsAbstract)  
                                     .Where(x => !x.IsGenericTypeDefinition) 
                                     .Where(x => typeof(MenuListItem).IsAssignableFrom(x));

            return q;
        }
        
    }

    public enum MenuItemTypes
    {
        Button,
        Toggle,
        Slider,
        Enum,
        InputField
    }
}