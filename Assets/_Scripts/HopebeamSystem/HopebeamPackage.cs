using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    [CreateAssetMenu(fileName = "New Hopebeam Package", menuName = "Hopebeams/Hopebeam Package", order = 0)]
    public class HopebeamPackage : ScriptableObject
    {
        public List<ConditionHolder> packageConditionHolders = new();
        public List<HopebeamCreation> hopebeamCreations = new();
    }
}