using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Toggle
{
    public class ToggleInteract : MonoBehaviour
    {
        public ToggleItem toggleItem;
        
        public void Interact(bool toggle)
        {
            toggleItem.Interact(toggle);
        }
    }
}