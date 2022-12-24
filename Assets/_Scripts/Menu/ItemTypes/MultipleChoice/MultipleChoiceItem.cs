using System.Collections.Generic;
using EraSoren.Menu.General;

namespace EraSoren.Menu.ItemTypes.MultipleChoice
{
    public abstract class MultipleChoiceItem : MenuItem
    {
        public List<MultipleChoiceOption> options = new();
        public MultipleChoiceOption currentOption;
        private int _currentOptionNo;

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