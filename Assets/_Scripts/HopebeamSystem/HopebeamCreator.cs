using System;
using EraSoren._Core.Helpers;
using Sirenix.OdinInspector;

namespace EraSoren.HopebeamSystem
{
    public class HopebeamCreator : Singleton<HopebeamCreator>
    {
        public HopebeamCreationMethodList activeCreationMethodList;

        [Button]
        public void ActivateCreation()
        {
            activeCreationMethodList.SetCreationActivity(true);
        }

        public void SetCreationActivity(bool active)
        {
            activeCreationMethodList.SetCreationActivity(active);
        }

        public void SetActiveCreationMethodList(HopebeamCreationMethodList methodList)
        {
            activeCreationMethodList.SetCreationActivity(false);
            activeCreationMethodList = methodList;
            activeCreationMethodList.SetCreationActivity(true);
        }

        public void RestartAllNextCreationTimes()
        {
            activeCreationMethodList.RestartAllNextCreationTimes();
        }
    }
}