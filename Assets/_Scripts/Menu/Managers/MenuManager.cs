using System;
using System.Collections.Generic;
using System.Linq;
using EraSoren._Core.Helpers;
using EraSoren.Menu.ItemTypes;
using EraSoren.Menu.ItemTypes.Button;
using UnityEngine;

namespace EraSoren.Menu.Managers
{
    public class MenuManager : Singleton<MenuManager>
    {
        public string menuNameSuffix = "MenuItem";
        public List<MenuButtonItem> menuHierarchy = new ();

        #region Events

        public delegate void CanvasActivityHandler(GameObject canvasObject);

        public event CanvasActivityHandler OnActivateCanvas;

        #endregion

        private void Start()
        {
            AddMenuItem(MenuLogicManager.I.firstSceneItem);
        }

        public void AddMenuItem(MenuButtonItem menuButtonItem)
        {
            menuHierarchy.Add(menuButtonItem);
        }

        public void RemoveLastMenuItem()
        {
            if (menuHierarchy.Count >= 1)
            {
                menuHierarchy.Remove(menuHierarchy.Last());
            }
        }

        public void DisableLastMenuItem()
        {
            if (menuHierarchy.Count >= 1) 
            {
                menuHierarchy.Last().Disable();
            }
        }

        public void EnableLastMenuItem()
        {
            if (menuHierarchy.Count >= 1) 
            {
                menuHierarchy.Last().Enable();
            }
        }

        public void SetActiveCanvas(GameObject canvasObject)
        {
            OnActivateCanvas?.Invoke(canvasObject);
        }
    }
}