using System.Collections.Generic;
using UnityEngine;

namespace EraSoren.Menu.General
{
    public class ItemsList : MonoBehaviour
    {
        public List<MenuItem> allItems = new();

        public void AddItem(MenuItem item)
        {
            if (!allItems.Contains(item))
            {
                allItems.Add(item);
            }
        }

        public void RemoveItem(MenuItem item)
        {
            allItems.Remove(item);
        }

        public void ClearItems()
        {
            allItems.Clear();
        }
    }
}