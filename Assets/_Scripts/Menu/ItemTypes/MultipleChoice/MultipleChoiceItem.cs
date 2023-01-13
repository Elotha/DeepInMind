using System.Collections.Generic;
using System.Linq;
using EraSoren.Menu.General;
using EraSoren.Menu.Managers;
using Sirenix.OdinInspector;
using TreeEditor;
using UnityEngine;

namespace EraSoren.Menu.ItemTypes.MultipleChoice
{
    public abstract class MultipleChoiceItem : MenuItem
    {
        public GameObject menuCanvasObject;
        public List<MultipleChoiceOption> options = new();
        public MultipleChoiceOption currentOption;
        private int _currentOptionNo;

        public void AddOption(MultipleChoiceOption newOption)
        {
            if (!options.Contains(newOption))
            {
                options.Add(newOption);
            }
        }

        public void SelectOption(MultipleChoiceOption selectedOption)
        {
            for (var i = 0; i < options.Count; i++)
            {
                if (options[i] != selectedOption) continue;
                SelectOptionByIndex(i);
            }
        }

        public void SelectNextOption()
        {
            SelectOptionByIndex((_currentOptionNo + 1) % options.Count);
        }

        public void SelectPreviousOption()
        {
            SelectOptionByIndex((_currentOptionNo - 1 + options.Count) % options.Count);
        }

        private void SelectOptionByIndex(int index)
        {
            _currentOptionNo = index;
            currentOption = options[index];
            currentOption.Activate();
        }
    }
}