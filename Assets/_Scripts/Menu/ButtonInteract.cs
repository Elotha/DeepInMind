using UnityEngine;

namespace EraSoren.Menu
{
    [ExecuteInEditMode]
    public class ButtonInteract : MonoBehaviour
    {
        public MenuItem menuItem;
        public delegate void OnClick();
        public event OnClick OnClickEvent;

        public void Interact()
        {
            OnClickEvent?.Invoke();
        }

    }
}