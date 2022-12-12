using EraSoren.Menu.ItemTypes.Toggle;
using UnityEngine;

namespace EraSoren.Menu.MenuItems
{
    public class TogyMI : ToggleItem
    {
        public override void Interact(bool toggle)
        {
            Debug.Log(toggle);
        }
    }
}