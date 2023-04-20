using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace EraSoren.HopebeamSystem
{
    [Serializable]
    public class PackageCreationOverride
    {
        public List<ConditionHolder> overrideConditionHolders;
        public HopebeamPackage hopebeamPackage;
    }
}