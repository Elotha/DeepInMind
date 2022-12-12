using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Button
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