using System.Collections.Generic;
using System.Linq;
using EraSoren._Core.Helpers;
using EraSoren.Menu.General;
using EraSoren.Menu.ItemTypes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.Managers
{
    public class LengthInHierarchyManager : Singleton<LengthInHierarchyManager>
    {
        [OnValueChanged(nameof(OnLengthInHierarchyChange))]
        public int lengthInHierarchy = 200;

        public void OnLengthInHierarchyChange()
        {
            foreach (var menuItemCreator in MenuLogicManager.I.menuItemCreators)
            {
                foreach (var item in menuItemCreator.currentItems
                                                .Where(item => !item.overrideLengthInHierarchy))
                {
                    item.lengthInHierarchy = lengthInHierarchy;
                }
            
                ChangeLengthInHierarchy(menuItemCreator.currentItems);
            }
        }

        public static void ChangeLengthInHierarchy(List<MenuItem> currentItems)
        {
            var newGap = Vector3.zero;
            newGap = currentItems.Aggregate(newGap, (current, menuListItem) => 
                                                current + menuListItem.lengthInHierarchy * Vector3.up);

            var creationPoint = newGap / 2;
            
            foreach (var item in currentItems)
            {
                creationPoint += item.lengthInHierarchy / 2f * Vector3.down;
                item.rectTransform.localPosition = creationPoint;
                creationPoint += item.lengthInHierarchy / 2f * Vector3.down;
            }
        }
    }
}