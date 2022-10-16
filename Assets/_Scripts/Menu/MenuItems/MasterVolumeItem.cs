using UnityEngine;
using UnityEngine.UI;

namespace EraSoren.Menu.MenuItems
{
    public class MasterVolumeItem : MenuItem
    {
        [SerializeField] private float volume;
        [SerializeField] private Slider slider;

        public override void Interact()
        {
            volume = slider.value;
        }
    }
}
