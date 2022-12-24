using System;
using System.Collections.Generic;
using EraSoren._Core.Helpers;
using EraSoren.Menu.ItemTypes.Button;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.Menu.Managers
{
    [ExecuteAlways]
    public class MenuLogicManager : Singleton<MenuLogicManager>
    {
        [Header("References")]
        public MenuButtonItem firstSceneItem;

        [Header("For Clearing")]
        [SerializeField] private Transform mainMenuLogic;
        [SerializeField] private Transform canvasParent;
        [SerializeField] private Transform mainMenuCanvas;

        [Header("Creation")]
        [OnValueChanged(nameof(ChangeOffset))] 
        [SerializeField] private Vector2 canvasOffset;

        public List<MenuItemCreator> menuItemCreators = new();

        #region Events

        public UnityEvent onClearingMenuItems;

        #endregion

        private void Start()
        {
            SetActiveCanvas(firstSceneItem.gameObject);
        }

        public void SetActiveCanvas(GameObject logicObject)
        {
            foreach (var menuItemCreator in menuItemCreators)
            {
                menuItemCreator.canvasMenuParent.gameObject.SetActive(menuItemCreator.gameObject == logicObject);
            }
        }

        public static string StandardizeNewMenuName(string menuName, bool removeSpaces)
        {
            var str = menuName.Replace(":", "");
            if (removeSpaces)
                str = str.Replace(" ", "");
            return str;
        }

        private void ChangeOffset()
        {
            // foreach (var logicObject in menuLogicObjects)
            // {
            //     logicObject.canvasObject.transform.localPosition = canvasOffset;
            // }
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
            
            ClearAllMenuItemsFromLists();
            onClearingMenuItems?.Invoke();
        }

        private static void ClearAllMenuItemsFromLists()
        {
            foreach (var itemTypeManager in ItemTypeManagers.I.itemTypes)
            {
                itemTypeManager.menuItemTypeManager.itemsList.ClearItems();
            }
        }

        [Serializable]
        public struct MenuDictionary
        {
            public string menuName;
            public MenuButtonItem menuButtonItem;

            public MenuDictionary(string menuName, MenuButtonItem menuButtonItem)
            {
                this.menuName = menuName;
                this.menuButtonItem = menuButtonItem;
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
    }
}