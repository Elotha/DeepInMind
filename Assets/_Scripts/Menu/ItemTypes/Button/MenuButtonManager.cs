using System.Threading.Tasks;
using EraSoren.Menu.General;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Button
{
    public class MenuButtonManager : MenuItemTypeManager
    {
        [Header("Objects")]
        [SerializeField] private GameObject menuButtonLogicPrefab;
        [SerializeField] private GameObject canvasParentPrefab;
        
        [Header("References")]
        [SerializeField] private Transform canvasMainParent;
        
        [OnValueChanged(nameof(ChangeOffset))] 
        [SerializeField] private Vector2 canvasOffset;

        [SerializeField] private string canvasSuffix = " Canvas";

        public override void CreateScript(string itemName)
        {
            CreateMenuScript.I.Create(itemName, nameof(MenuButtonItem), MenuItemTypes.MenuButton);
        }

        public override void CreateObjects(string itemName, Transform parentObject)
        {
            var menuLogicObject = CreateMenuLogicObject(itemName, parentObject);
            var newCanvasParent = CreateCanvasParent(itemName);
            SetValuesOfItemCreator(menuLogicObject, newCanvasParent);
            AssignCanvasParentProperties(newCanvasParent);
        }
        
        
        public override MenuItem Finalize(GameObject obj, string itemName, Transform canvasMenuParent)
        {
            // TODO: Button manager üzerinden create yapma işlemini daha sağlıklı hale getirmem lazım
            
            var newItem = ButtonManager.I.FinalizeItem(obj, itemName, canvasMenuParent);
            var buttonItem = obj.GetComponent<MenuButtonItem>();
            buttonItem.menuCanvasObject = obj.GetComponent<MenuItemCreator>().canvasMenuParent.gameObject;
            
            return newItem;
        }

        private GameObject CreateCanvasParent(string itemName)
        {
            var newCanvasParent = Instantiate(canvasParentPrefab, canvasMainParent);
            newCanvasParent.name = itemName + canvasSuffix;
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
            var menuLogicObject = Instantiate(menuButtonLogicPrefab, parentObject);
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