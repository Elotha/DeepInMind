using System;
using System.IO;
using System.Threading.Tasks;
using EraSoren._Core.Helpers;
using EraSoren.Menu.Managers;
using UnityEditor;
using UnityEngine;

namespace EraSoren.Menu.General
{
    public class CreateMenuScript : Singleton<CreateMenuScript>
    {
        [SerializeField] private string filePath = "Assets/_Scripts/Menu/MenuItems";
        [SerializeField] private string namespaceName = "Menu.MenuItems";
        public GetNamespaceForMenuItemTypes getNamespaceForMenuItemTypes;
        
        public void Create(string scriptName, string ancestorName, MenuItemTypes itemType) 
        {
            scriptName = MenuLogicManager.StandardizeNewMenuName(scriptName, true);
            scriptName += MenuManager.I.menuNameSuffix;
            
            var namespaceSuffix = GetNamespaceForMenuItemTypes.GetNamespace(itemType);
            var copyPath = filePath + "/" + scriptName + ".cs";

            if (File.Exists(copyPath)) return;
            
            using var outfile = new StreamWriter(copyPath);
            outfile.Write("using EraSoren.Menu.ItemTypes." + namespaceSuffix + ";" +
                          "\n\n" +
                          "namespace " + namespaceName +
                          "\n{" + 
                          "\n    public class "+scriptName+" : " + ancestorName +
                          "\n    {" +
                          "\n        " +
                          "\n    }" +
                          "\n}");
            outfile.Close();
        }

        public MenuItem AddMenuComponent(string scriptName, GameObject scriptObject)
        {
            // Debug.Log(namespaceName + "." + scriptName);
            return scriptObject.AddComponent(Type.GetType(namespaceName + "." + scriptName)) as MenuItem;
        }
    }
}