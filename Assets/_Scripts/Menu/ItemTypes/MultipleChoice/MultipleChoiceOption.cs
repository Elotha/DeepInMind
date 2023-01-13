using EraSoren.Menu.General;
using EraSoren.Menu.ItemTypes.Button;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.MultipleChoice
{
    public abstract class MultipleChoiceOption : ButtonItem
    {
        public virtual void Activate() { }
    }
}