using EraSoren.Menu.ItemTypes;
using EraSoren.Menu.Managers;

namespace EraSoren.Menu.MenuItems
{
    public class BackMI : StandardButtonItem
    {
        public override void Interact()
        {
            MenuManager.I.DisableLastMenuItem();
            MenuManager.I.RemoveLastMenuItem();
            MenuManager.I.EnableLastMenuItem();
        }
    }
}