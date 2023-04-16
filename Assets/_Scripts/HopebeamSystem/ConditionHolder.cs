using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    [Serializable]
    public class ConditionHolder
    {
        [SerializeReference] public List<ICondition> creationMethodConditions = new();

        public bool AreConditionsMet()
        {
            return creationMethodConditions.All(condition => condition.EvaluateCondition());
        }

        public static bool EvaluateConditionHolders(IEnumerable<ConditionHolder> conditionHolder)
        {
            return conditionHolder.Any(holder => holder.AreConditionsMet());
        }
    }
}