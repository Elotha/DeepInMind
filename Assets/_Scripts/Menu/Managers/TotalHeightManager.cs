using System.Collections.Generic;
using System.Linq;
using EraSoren._Core.Helpers;
using EraSoren.Menu.General;
using EraSoren.Menu.ItemTypes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.Managers
{
    public class TotalHeightManager : Singleton<TotalHeightManager>
    {
        [OnValueChanged("OnTotalHeightChange")]
        public int totalHeight = 200;

        public void OnTotalHeightChange()
        {
            foreach (var menuItemCreator in MenuLogicManager.I.menuItemCreators)
            {
                foreach (var item in menuItemCreator.currentItems
                                                .Where(item => !item.overrideTotalHeight))
                {
                    item.totalHeight = totalHeight;
                }
            
                ChangeTotalHeight(menuItemCreator.currentItems);
            }
        }

        public static void ChangeTotalHeight(List<MenuItem> currentItems)
        {
            var newGap = Vector3.zero;
            newGap = currentItems.Aggregate(newGap, (current, menuListItem) => 
                                                current + menuListItem.totalHeight * Vector3.up);

            var creationPoint = newGap / 2;
            
            foreach (var item in currentItems)
            {
                creationPoint += item.totalHeight / 2f * Vector3.down;
                item.rectTransform.localPosition = creationPoint;
                creationPoint += item.totalHeight / 2f * Vector3.down;
            }
        }
    }
}