﻿using System.Collections.Generic;
using EraSoren.Menu.ItemTypes;
using UnityEngine;

namespace EraSoren.Menu.ItemLists
{
    public class SliderItemsList : MonoBehaviour
    {
        public List<SliderItem> allSliderItems = new();

        public void AddItem(SliderItem buttonItem)
        {
            if (!allSliderItems.Contains(buttonItem))
            {
                allSliderItems.Add(buttonItem);
            }
        }

        public void RemoveItem(SliderItem buttonItem)
        {
            allSliderItems.Remove(buttonItem);
        }

        public void ClearItems()
        {
            allSliderItems.Clear();
        }
    }
}