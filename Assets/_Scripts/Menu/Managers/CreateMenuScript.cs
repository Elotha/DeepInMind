using System;
using System.IO;
using EraSoren._Core.Helpers;
using UnityEditor;
using UnityEngine;
using MenuItem = EraSoren.Menu.ItemTypes.MenuItem;

namespace EraSoren.Menu.Managers
{
    public class CreateMenuScript : Singleton<CreateMenuScript>
    {
        [SerializeField] private string filePath = "Assets/_Scripts/Menu/MenuItems";
        [SerializeField] private string namespaceName = "Menu.MenuItems";
        
        public void Create(string scriptName, string ancestorName) 
        {
            scriptName = MenuLogicManager.StandardizeNewMenuName(scriptName, true);
            scriptName += MenuManager.I.menuNameSuffix;
            var copyPath = filePath + "/" + scriptName + ".cs";
            
            if (!File.Exists(copyPath)) {
                
                using var outfile = new StreamWriter(copyPath);

                outfile.Write("using EraSoren.Menu.ItemTypes;" +
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
            AssetDatabase.Refresh();
        }

        public MenuItem AddMenuComponent(string scriptName, GameObject scriptObject)
        {
            // Debug.Log(namespaceName + "." + scriptName);
            return scriptObject.AddComponent(Type.GetType(namespaceName + "." + scriptName)) as MenuItem;
        }
    }
}