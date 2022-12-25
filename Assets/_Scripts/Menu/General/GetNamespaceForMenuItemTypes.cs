using EraSoren.Menu.Managers;
using UnityEngine;

namespace EraSoren.Menu.General
{
    public class GetNamespaceForMenuItemTypes : MonoBehaviour
    {
        public static string GetNamespace(MenuItemTypes menuItemType)
        {
            switch (menuItemType)
            {
                case MenuItemTypes.StandardButton:
                case MenuItemTypes.MenuButton:
                    return "Button";
                
                case MenuItemTypes.Toggle:
                    return "Toggle";
                
                case MenuItemTypes.Slider:
                    return "Slider";
                
                case MenuItemTypes.MultipleChoice:
                    return "MultipleChoice";
                
                case MenuItemTypes.InputField:
                    return "InputField";
                
                default:
                    Debug.LogError("Provided item type does not have specified namespace!");
                    return "";
            }
        }
    }
}