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
        // public List<MenuItem> buttonList = new List<MenuItem>();

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
            MenuManager.I.DisableLast();
            Enable();
            MenuManager.I.AddItem(this);
        }

        public virtual void Interact(bool toggle)
        {
            
        }
        
        public virtual void Back() {
            Disable();
            MenuManager.I.RemoveItem(this);
            MenuManager.I.EnableLast();
        }
    }
}