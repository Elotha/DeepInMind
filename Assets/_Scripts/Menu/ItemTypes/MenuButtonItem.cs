using EraSoren.Menu.Managers;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes
{
    public abstract class MenuButtonItem : ButtonItem
    {
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
    }
}