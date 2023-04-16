using System;
using System.Collections.Generic;
using System.Linq;

namespace EraSoren.HopebeamSystem
{
    [Serializable]
    public class HopebeamPackage
    {
        public List<ConditionHolder> packageConditionHolders = new();
        public List<HopebeamCreation> hopebeamCreations = new();
    }
}