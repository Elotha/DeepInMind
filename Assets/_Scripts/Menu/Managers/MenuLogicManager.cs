using System;
using System.Collections.Generic;
using System.Linq;
using EraSoren._Core.Helpers;
using EraSoren._Core.Helpers.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.Managers
{
    public class MenuLogicManager : Singleton<MenuLogicManager>
    {
        [Header("References")]
        public GameObject firstSceneItem;

        [Header("For Clearing")]
        [SerializeField] private Transform mainMenuLogic;
        [SerializeField] private Transform canvasParent;
        [SerializeField] private Transform mainMenuCanvas;

        [Header("Creation")]
        [OnValueChanged(nameof(ChangeOffset))] [SerializeField] private Vector2 canvasOffset;

        // [Space(20)]
        // public List<MenuLogicObject> menuLogicObjects = new ();
        
        public List<MenuDictionary> menusDictionary;
        public List<MenuItemCreator> menuItemCreators = new();

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

        public static string StandardizeNewMenuName(string menuName, bool removeSpaces)
        {
            var str = menuName.Replace(":", "");
            if (removeSpaces)
                str = str.Replace(" ", "");
            return str;
        }

        private void SetCanvasActivityOnEdit()
        {
            // TODO: Use event for this
        }

        public void AddNewMenuDictionary(string itemName, MenuItem menuItem)
        {
            menusDictionary.Add(new MenuDictionary(itemName, menuItem));
        }

        public void SetActiveCanvas(GameObject logicObject)
        {
            foreach (var menuItemCreator in menuItemCreators)
            {
                menuItemCreator.canvasMenuParent.gameObject.SetActive(menuItemCreator.gameObject == logicObject);
            }
        }

        private void ChangeOffset()
        {
            // foreach (var logicObject in menuLogicObjects)
            // {
            //     logicObject.canvasObject.transform.localPosition = canvasOffset;
            // }
        }

        private void Start()
        {
            SetCanvasActivityOnStart();
            InitializeMenuHieracrhy();
        }

        private void SetCanvasActivityOnStart()
        {
            // foreach (var item in menuLogicObjects) 
            // {
            //     item.canvasObject.SetActive(item.logicObject == firstSceneItem);
            // }
        }

        private void InitializeMenuHieracrhy()
        {
            foreach (var item in menusDictionary
                .Where(item => item.menuName == firstSceneItem.name + MenuManager.I.menuNameSuffix)) 
            {
                MenuManager.I.AddMenuItem(item.menuItem);
            }
        }

        [Button]
        public void Clear()
        {
            // TODO: Delete the files in MenuItems directory as well

            for (var i = mainMenuLogic.childCount; i > 0; i--)
            {
                DestroyImmediate(mainMenuLogic.GetChild(i-1).gameObject);
            }

            var menuItemCreator = mainMenuLogic.GetComponent<MenuItemCreator>();
            menuItemCreator.currentItems.Clear();

            for (var i = canvasParent.childCount; i > 0; i--)
            {
                if (canvasParent.GetChild(i-1) == mainMenuCanvas) continue;
                
                DestroyImmediate(canvasParent.GetChild(i-1).gameObject);
            }

            for (var i = mainMenuCanvas.childCount; i > 0; i--)
            {
                DestroyImmediate(mainMenuCanvas.GetChild(i-1).gameObject);
            }

            for (var j = menuItemCreators.Count - 1; j > 0; j--)
            {
                menuItemCreators.RemoveAt(j);
            }
        }
    }
}