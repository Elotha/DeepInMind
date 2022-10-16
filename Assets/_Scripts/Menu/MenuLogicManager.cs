using System;
using System.Collections.Generic;
using System.Linq;
using EraSoren._Core.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu
{
    public class MenuLogicManager : Singleton<MenuLogicManager>
    {
        [Header("Objects")]
        [SerializeField] private GameObject menuLogicPrefab;
        [SerializeField] private GameObject canvasParentPrefab;
        
        [Header("References")]
        [SerializeField] private Transform managerParent;
        [SerializeField] private Transform canvasMainParent;
        private GameObject _newCanvasParent;
        private GameObject _newMenuLogicObject;
        public GameObject firstSceneItem;
        
        [Header("Creation")]
        [SerializeField] private string newMenuName;
        [SerializeField] private bool setActiveNewMenu;
        [OnValueChanged("ChangeOffset")] [SerializeField] private Vector2 canvasOffset;

        [Space(20)]
        public List<MenuLogicObject> menuLogicObjects = new List<MenuLogicObject>();
        
        public List<MenuDictionary> menusDictionary;

        [Serializable]
        public struct MenuDictionary
        {
            public string menuName;
            public MenuItem menuItem;

            public MenuDictionary(string menuName, MenuItem menuItem)
            {
                this.menuName = menuName;
                this.menuItem = menuItem;
            }
        }

        [Serializable]
        public struct MenuLogicObject
        {
            public string menuName;
            public GameObject logicObject;
            public GameObject canvasObject;
            public MenuItemCreator menuItemCreator;

            public MenuLogicObject(string menuName, GameObject logicObject, GameObject canvasObject, 
                                   MenuItemCreator menuItemCreator)
            {
                this.menuName = menuName;
                this.logicObject = logicObject;
                this.canvasObject = canvasObject;
                this.menuItemCreator = menuItemCreator;
            }
        }

        [Button("Create Menu")]
        private void CreateMenu()
        {
            newMenuName = StandardizeNewMenuName(newMenuName);

            // Check the name to see if provided
            if (newMenuName == "") {
                Debug.LogError("You have to provide a name!");
                return;
            }
            
            // Check the menu logic objects
            if (menuLogicObjects.Any(menuLogicObject => menuLogicObject.menuName == newMenuName)) {
                Debug.LogError("The provided name already exists!");
                return;
            }
            
            CreateMenuLogicObject();

            CreateCanvasParent();

            var itemCreator = _newMenuLogicObject.GetComponent<MenuItemCreator>();
            itemCreator.canvasMenuParent = _newCanvasParent.transform;
            itemCreator.canvasOffset = canvasOffset;

            AddMenuLogicObject(itemCreator);

            AssignCanvasParentProperties();

            // Create a script for menu logic
            CreateMenuScript.I.Create(newMenuName); 
            
            // If there is already a menu logic script
            CheckExistingMenuItems();

            SetCanvasActivityOnEdit();

            AssignFirstSceneItemIfNull();

            // Log message
            Debug.Log("New menu objects have been created!");
        }

        private void AssignFirstSceneItemIfNull()
        {
            if (firstSceneItem == null)
            {
                firstSceneItem = _newMenuLogicObject;
            }
        }

        private void CheckExistingMenuItems()
        {
            foreach (var item in menusDictionary
                .Where(item => item.menuName == newMenuName + MenuManager.MenuNameSuffix)) {
                item.menuItem.menuObject = _newCanvasParent;
                break;
            }
        }

        public static string StandardizeNewMenuName(string menuName)
        {
            // Delete any spaces
            return menuName.Replace(" ", "").Replace(":", "");
        }

        private void CreateMenuLogicObject()
        {
            _newMenuLogicObject = Instantiate(menuLogicPrefab, managerParent);
            _newMenuLogicObject.name = newMenuName;
        }

        private void CreateCanvasParent()
        {
            _newCanvasParent = Instantiate(canvasParentPrefab, canvasMainParent);
            _newCanvasParent.name = newMenuName + "Parent";
        }

        private void AddMenuLogicObject(MenuItemCreator menuItemCreator)
        {
            menuLogicObjects.Add(new MenuLogicObject(newMenuName, _newMenuLogicObject, _newCanvasParent, 
                                                     menuItemCreator));
        }

        private void SetCanvasActivityOnEdit()
        {
            if (!setActiveNewMenu) {
                _newCanvasParent.SetActive(false);
            }
            else {
                foreach (var menuLogicObject in menuLogicObjects
                    .Where(x => x.menuName != newMenuName)) {
                    menuLogicObject.canvasObject.SetActive(false);
                }
            }
        }

        private void AssignCanvasParentProperties()
        {
            var parentRectTransform = _newCanvasParent.GetComponent<RectTransform>();
            parentRectTransform.pivot = new Vector2(0.5f, 0.5f);
            parentRectTransform.localPosition = Vector2.zero + canvasOffset;
        }

        [Button("Add Script to Menu Object")]
        private void AddScriptToMenu()
        {
            var logicMenuName = newMenuName + MenuManager.MenuNameSuffix;
            if (menusDictionary.Any(x => x.menuName == newMenuName)) {
                Debug.LogError("The provided script has already been added!");
                return;
            }
            
            // Add the menu logic script to the menu object
            var component = CreateMenuScript.I.AddMenuComponent(logicMenuName, _newMenuLogicObject);
            
            
            if (component == null) {
                Debug.LogError("it is null");
                return;
            }
            
            // Give reference to canvas object in menu logic script
            if (component != null) component.menuObject = _newCanvasParent;
            
            // Add menu logic script to a struct list
            AddNewMenuDictionary(newMenuName, component);
            
            // Log message
            Debug.Log("Script has been added to the menu logic object!");
            
        }

        public void AddNewMenuDictionary(string itemName, MenuItem menuItem)
        {
            menusDictionary.Add(new MenuDictionary(itemName, menuItem));
        }

        [Button]
        public void Clear()
        {
            foreach (var logicObject in menuLogicObjects) {
                DestroyImmediate(logicObject.canvasObject);
                DestroyImmediate(logicObject.logicObject);
            }
            menusDictionary = new List<MenuDictionary>();
            menuLogicObjects = new List<MenuLogicObject>();
        }

        private void ChangeOffset()
        {
            foreach (var logicObject in menuLogicObjects)
            {
                logicObject.canvasObject.transform.localPosition = canvasOffset;
            }
        }

        private void Start()
        {
            SetCanvasActivityOnStart();
            InitializeMenuHieracrhy();
        }

        private void SetCanvasActivityOnStart()
        {
            foreach (var item in menuLogicObjects) {
                item.canvasObject.SetActive(item.logicObject == firstSceneItem);
            }
        }

        public void SetCanvasActivityOnButton(GameObject canvasObject)
        {
            foreach (var logicObject in menuLogicObjects)
            {
                logicObject.canvasObject.SetActive(logicObject.canvasObject == canvasObject);
            }
        }

        private void InitializeMenuHieracrhy()
        {
            foreach (var item in menusDictionary
                .Where(item => item.menuName == firstSceneItem.name + MenuManager.MenuNameSuffix)) {
                MenuManager.I.AddItem(item.menuItem);
            }
        }
    }
}