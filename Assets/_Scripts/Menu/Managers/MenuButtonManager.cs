using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.Managers
{
    public class MenuButtonManager : ButtonManager
    {
        [Header("Objects")]
        [SerializeField] private GameObject menuButtonPrefab;
        [SerializeField] private GameObject canvasParentPrefab;
        
        [Header("References")]
        [SerializeField] private Transform canvasMainParent;
        
        [OnValueChanged(nameof(ChangeOffset))] 
        [SerializeField] private Vector2 canvasOffset;
        
        public override MenuListItem Create(string itemName, Transform canvasMenuParent, Transform parentObject)
        {
            var newItem = base.Create(itemName, canvasMenuParent, parentObject);
            
            var menuLogicObject = CreateMenuLogicObject(itemName, parentObject);
            
            var newCanvasParent = CreateCanvasParent(itemName);

            SetValuesOfItemCreator(menuLogicObject, newCanvasParent);

            AssignCanvasParentProperties(newCanvasParent);

            return newItem;
        }

        private GameObject CreateCanvasParent(string itemName)
        {
            var newCanvasParent = Instantiate(canvasParentPrefab, canvasMainParent);
            newCanvasParent.name = itemName + " Parent";
            return newCanvasParent;
        }

        private void SetValuesOfItemCreator(GameObject menuLogicObject, GameObject newCanvasParent)
        {
            var itemCreator = menuLogicObject.GetComponent<MenuItemCreator>();
            itemCreator.canvasMenuParent = newCanvasParent.transform;
            itemCreator.canvasOffset = canvasOffset;
            MenuLogicManager.I.menuItemCreators.Add(itemCreator);
        }

        private GameObject CreateMenuLogicObject(string itemName, Transform parentObject)
        {
            var menuLogicObject = Instantiate(menuButtonPrefab, parentObject);
            menuLogicObject.name = itemName;
            return menuLogicObject;
        }

        private void ChangeOffset()
        {
            // foreach (var logicObject in menuLogicObjects)
            // {
            //     logicObject.canvasObject.transform.localPosition = canvasOffset;
            // }
        }

        private void AssignCanvasParentProperties(GameObject newCanvasParent)
        {
            var parentRectTransform = newCanvasParent.GetComponent<RectTransform>();
            parentRectTransform.pivot = new Vector2(0.5f, 0.5f);
            parentRectTransform.localPosition = Vector2.zero + canvasOffset;
        }
    }
}