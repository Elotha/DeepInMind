using System.Collections.Generic;
using System.Linq;
using EraSoren._Core.Helpers;
using UnityEngine;

namespace EraSoren.Menu.Managers
{
    public class MenuManager : Singleton<MenuManager>
    {
        public string menuNameSuffix = "MenuItem";
        public List<MenuItem> menuHierarchy = new ();

        #region Events

        public delegate void CanvasActivityHandler(GameObject canvasObject);

        public event CanvasActivityHandler OnActivateCanvas;

        #endregion

        public void AddMenuItem(MenuItem menuItem)
        {
            menuHierarchy.Add(menuItem);
        }

        public void RemoveMenuItem(MenuItem menuItem)
        {
            menuHierarchy.Remove(menuItem);
        }

        public void DisableLastMenuItem()
        {
            if (menuHierarchy.Count >= 1) 
            {
                menuHierarchy.Last().menuObject.SetActive(false);
            }
        }

        public void EnableLastMenuItem()
        {
            if (menuHierarchy.Count >= 1) 
            {
                menuHierarchy.Last().menuObject.SetActive(true);
            }
        }

        public void SetActiveCanvas(GameObject canvasObject)
        {
            OnActivateCanvas?.Invoke(canvasObject);
        }
    }
}