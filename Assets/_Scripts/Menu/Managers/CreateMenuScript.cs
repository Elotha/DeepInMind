using System;
using System.IO;
using EraSoren._Core.Helpers;
using EraSoren.Menu.ItemTypes;
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

                outfile.WriteLine("using EraSoren.Menu.ItemTypes;");
                outfile.WriteLine("namespace " + namespaceName);
                outfile.WriteLine("{");
                outfile.WriteLine("    public class "+scriptName+" : " + ancestorName);
                outfile.WriteLine("    {");
                outfile.WriteLine("        ");
                outfile.WriteLine("    }");
                outfile.WriteLine("}"); 
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