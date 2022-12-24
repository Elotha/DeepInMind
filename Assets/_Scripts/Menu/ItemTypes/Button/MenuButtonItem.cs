using EraSoren.Menu.Managers;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Button
{
    public abstract class MenuButtonItem : ButtonItem
    {
        protected MenuButtonItem()
        {
            itemType = MenuItemTypes.MenuButton;
        }
        
        public GameObject menuCanvasObject;

        public void Enable()
        {
            menuCanvasObject.SetActive(true);
        }

        public void Disable()
        {
            menuCanvasObject.SetActive(false);
        }
        public override void Interact()
        {
            MenuManager.I.DisableLastMenuItem();
            Enable();
            MenuManager.I.AddMenuItem(this);
        }

        protected override void OnDestroy()
        {
            DestroyImmediate(menuCanvasObject);
            base.OnDestroy();
        }
    }
}