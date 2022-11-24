using EraSoren.Menu.Managers;
using UnityEngine;

namespace EraSoren.Menu
{
    public class MenuItem : MonoBehaviour
    {
        public MenuItem(GameObject menuObject)
        {
            this.menuObject = menuObject;
        }
        public GameObject menuObject;

        protected MenuItem()
        {
            
        }

        private void Enable()
        {
            Debug.Log(menuObject.name);
            menuObject.SetActive(true);
        }

        private void Disable()
        {
            menuObject.SetActive(false);
        }
        public virtual void Interact()
        {
            Debug.Log("menu item interact");
            MenuManager.I.DisableLastMenuItem();
            Enable();
            MenuManager.I.AddMenuItem(this);
        }

        public virtual void Interact(bool toggle)
        {
        }
        
        public virtual void Back() 
        {
            Disable();
            MenuManager.I.RemoveMenuItem(this);
            MenuManager.I.EnableLastMenuItem();
        }
    }
}