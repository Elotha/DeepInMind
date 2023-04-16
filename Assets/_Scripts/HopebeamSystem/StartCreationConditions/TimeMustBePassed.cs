using System;
using UnityEngine;

namespace EraSoren.HopebeamSystem.StartCreationConditions
{
    [Serializable]
    public class TimeMustBePassed : ICondition
    {
        [SerializeField] private float time = 0.1f;
        public bool EvaluateCondition()
        {
            return time > 0.2f;
        }
    }
}