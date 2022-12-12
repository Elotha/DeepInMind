using System.Collections.Generic;
using EraSoren.Menu.ItemTypes;
using UnityEngine;

namespace EraSoren.Menu.ItemLists
{
    public class ButtonItemsList : MonoBehaviour
    {
        public List<ButtonItem> allButtonItems = new();

        public void AddItem(ButtonItem buttonItem)
        {
            if (!allButtonItems.Contains(buttonItem))
            {
                allButtonItems.Add(buttonItem);
            }
        }

        public void RemoveItem(ButtonItem buttonItem)
        {
            allButtonItems.Remove(buttonItem);
        }

        public void ClearItems()
        {
            allButtonItems.Clear();
        }
    }
}