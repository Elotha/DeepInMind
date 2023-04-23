using System;
using EraSoren.Other;
using UnityEngine;

namespace EraSoren.HopebeamSystem.StartCreationConditions
{
    [Serializable]
    public class TimeMustBePassed : ICondition
    {
        [field: SerializeField]
        public bool IsActive { get; set; }
        
        public float requiredTime;

        public bool EvaluateCondition()
        {
            return TimeManager.I.levelTime > requiredTime;
        }
    }
}