using System.Collections.Generic;
using System.Linq;
using EraSoren._Core.Helpers;

namespace EraSoren.Menu
{
    public class MenuManager : Singleton<MenuManager>
    {
        public const string MenuNameSuffix = "Item";
        public List<MenuItem> menuHierarchy = new List<MenuItem>();
        public bool denemeBool = true;

        public void AddItem(MenuItem item)
        {
            menuHierarchy.Add(item);
        }

        public void RemoveItem(MenuItem item)
        {
            menuHierarchy.Remove(item);
        }

        public void DisableLast()
        {
            if (menuHierarchy.Count >= 1) {
                menuHierarchy.Last().menuObject.SetActive(false);
            }
        }

        public void EnableLast()
        {
            if (menuHierarchy.Count >= 1) {
                menuHierarchy.Last().menuObject.SetActive(true);
            }
        }
    }
}