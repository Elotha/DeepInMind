using System;
using EraSoren.Other;
using UnityEngine;

namespace EraSoren.HopebeamSystem.StartCreationConditions
{
    [Serializable]
    public class TimeMustBePassed : ICondition
    {
        public float requiredtime;
        public bool EvaluateCondition()
        {
            return TimeManager.I.levelTime > requiredtime;
        }
    }
}