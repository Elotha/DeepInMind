using EraSoren.Menu.Managers;
using UnityEngine;

namespace EraSoren.Menu.General
{
    public class GetNamespaceForMenuItemTypes : MonoBehaviour
    {
        public static string GetNamespace(MenuItemTypes menuItemType)
        {
            return menuItemType switch
            {
                MenuItemTypes.StandardButton => "Button",
                MenuItemTypes.MenuButton => "Button",
                MenuItemTypes.Toggle => "Toggle",
                MenuItemTypes.Slider => "Slider",
                MenuItemTypes.MultipleChoice => "MultipleChoice",
                MenuItemTypes.InputField => "InputField",
                _ => ""
            };
        }
    }
}