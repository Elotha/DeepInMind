using System.IO;
using EraSoren.Menu.General;
using EraSoren.Menu.Managers;
using UnityEditor;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.MultipleChoice
{
    public class CreateEnumScript : MonoBehaviour
    {
        [SerializeField] private string filePath = "Assets/_Scripts/Menu/MultipleChoice/Enums";
        [SerializeField] private string namespaceName = "Menu.MultipleChoice.Enums";
        
        public void Create(string scriptName, string enumPrefix) 
        {
            scriptName = MenuLogicManager.StandardizeNewMenuName(scriptName, true);
            scriptName += MenuManager.I.menuNameSuffix;
            
            var copyPath = filePath + "/" + enumPrefix + "/" + scriptName + ".cs";
            
            if (!File.Exists(copyPath)) {
                
                using var outfile = new StreamWriter(copyPath);

                outfile.Write("namespace " + namespaceName +
                              "\n{" + 
                              "\n    public enum "+ scriptName +
                              "\n    {" +
                              "\n        " +
                              "\n    }" +
                              "\n}");
                outfile.Close(); 
            }
            AssetDatabase.Refresh();
        }
    }
}