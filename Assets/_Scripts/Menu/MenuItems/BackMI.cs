using EraSoren.Menu.ItemTypes.Button;
using EraSoren.Menu.Managers;

namespace EraSoren.Menu.MenuItems
{
    public class BackMI : StandardButtonItem
    {
        public override void Interact()
        {
            MenuManager.I.RemoveLastMenuItem();
        }
    }
}