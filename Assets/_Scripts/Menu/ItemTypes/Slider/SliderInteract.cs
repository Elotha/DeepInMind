using UnityEngine;

namespace EraSoren.Menu.ItemTypes.Slider
{
    public class SliderInteract : MonoBehaviour
    {
        public SliderItem sliderItem;
        
        public void Interact(float value)
        {
            sliderItem.Interact(value);
        }
    }
}