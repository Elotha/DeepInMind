using System.IO;
using EraSoren._Core.Helpers;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace EraSoren.Menu.General
{
    public class DeleteObsoleteMenuItems : Singleton<DeleteObsoleteMenuItems>
    {
        public bool deleteIfObsolete;
        [SerializeField] private string directoryPath = "Assets/_Scripts/Menu/MenuItems/";

        public void DeleteObsoleteFile(string itemName)
        {
            if (Application.isPlaying) return;
            if (!deleteIfObsolete) return;
            
            itemName = itemName.Replace(" ", "");
            var filePath = directoryPath + itemName + MenuManager.I.menuNameSuffix + ".cs";
            Debug.Log(filePath);
            if (!File.Exists(filePath))
            {
                Debug.LogError("File path does not exist!");
                return;
            }
            File.Delete(filePath);
        }

        [Button]
        private void DeleteMenuItemsExceptMainMenu()
        {
            if (Application.isPlaying) return;
            
            var files = Directory.GetFiles(directoryPath);
            if (files.Length > 1)
            {
                foreach (var file in files)
                {
                    if (file.Contains("MainMenu" + MenuManager.I.menuNameSuffix)) continue;
                    File.Delete(file);
                }
                AssetDatabase.Refresh();
            }
            else
            {
                Debug.LogError("There is only Main Menu script!");
            }
        }
    }
}