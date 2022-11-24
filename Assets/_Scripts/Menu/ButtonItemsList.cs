using System.Collections.Generic;
using UnityEngine;

namespace EraSoren.Menu
{
    public class ButtonItemsList : MonoBehaviour
    {
        public List<ButtonListItem> AllButtonItems = new();

        public void AddItem(ButtonListItem buttonListItem)
        {
            if (!AllButtonItems.Contains(buttonListItem))
            {
                AllButtonItems.Add(buttonListItem);
            }
        }

        public void RemoveItem(ButtonListItem buttonListItem)
        {
            if (AllButtonItems.Contains(buttonListItem))
            {
                AllButtonItems.Remove(buttonListItem);
            }
        }

        public void ClearItems()
        {
            AllButtonItems.Clear();
        }
    }
}