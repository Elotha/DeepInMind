using EraSoren.Menu.ItemTypes;
using UnityEngine;

namespace EraSoren.Menu
{
    [ExecuteInEditMode]
    public class ButtonInteract : MonoBehaviour
    {
        public ButtonItem buttonItem;

        public void Interact()
        {
            buttonItem.Interact();
        }

    }
}