using EraSoren.Menu.ItemTypes.Button;
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