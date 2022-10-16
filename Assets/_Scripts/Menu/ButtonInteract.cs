using UnityEngine;
using UnityEngine.UI;

namespace EraSoren.Menu
{
    [ExecuteInEditMode]
    public class ButtonInteract : MonoBehaviour
    {
        private Button _button;
        private MenuItem _menuItem;

        public delegate void OnClick();

        public OnClick onClickEvent;

        private void Awake()
        {
            _menuItem = GetComponent<MenuItem>();
            _button = GetComponent<Button>();
        }

        public void Interact()
        {
            onClickEvent?.Invoke();
        }

    }
}