using System.Threading.Tasks;
using EraSoren._Core.Helpers;
using EraSoren.Menu.ItemTypes.MultipleChoice;
using EraSoren.Menu.Managers;
using UnityEditor.Callbacks;
using UnityEngine;

namespace EraSoren.Menu.General
{
    [ExecuteAlways]
    public class ReloadScripts : Singleton<ReloadScripts>
    {
        private bool _runOnReload;
        private ItemCreator _menuItemCreator;
        
        [DidReloadScripts]
        public static void Reload()
        {
            if (I == null) return;
            I.OnReload();
        }

        private void OnReload()
        {
            if (!_runOnReload) return;
            
            _runOnReload = false;
            _menuItemCreator.ContinueAfterRefreshingAssets();
            _menuItemCreator = null;
        }

        public void Set(ItemCreator itemCreator)
        {
            _runOnReload = true;
            _menuItemCreator = itemCreator;
        }
    }
}