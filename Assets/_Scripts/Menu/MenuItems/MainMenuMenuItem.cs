using System;
using EraSoren.Menu.Managers;

namespace EraSoren.Menu.MenuItems
{
    public class MainMenuMenuItem : MenuItem
    {
        private void Start()
        {
            MenuManager.I.AddMenuItem(this);
        }
    }
}