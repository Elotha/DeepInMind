using System.Collections.Generic;
using EraSoren.Menu.ItemTypes.Slider;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Toggle
{
    public class ToggleItemsList : MonoBehaviour
    {
        public List<ToggleItem> allToggleItems = new();

        public void AddItem(ToggleItem buttonItem)
        {
            if (!allToggleItems.Contains(buttonItem))
            {
                allToggleItems.Add(buttonItem);
            }
        }

        public void RemoveItem(ToggleItem buttonItem)
        {
            allToggleItems.Remove(buttonItem);
        }

        public void ClearItems()
        {
            allToggleItems.Clear();
        }
    }
}