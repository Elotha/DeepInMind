using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    [Serializable]
    public class ConditionHolder
    {
        public bool isActive = true;
        [SerializeReference] public List<ICondition> conditions = new();

        public static ConditionResult EvaluateConditionHolders(List<ConditionHolder> conditionHolders)
        {
            if (conditionHolders.All(x => !x.isActive))
            {
                return ConditionResult.NoActiveConditionHolders;
            }
            
            var result = conditionHolders.Where(x => x.isActive).Any(y => y.AreConditionsMet());
            return result ? ConditionResult.ConditionsAreMet : ConditionResult.ConditionsAreNotMet;
        }

        public bool AreConditionsMet()
        {
            return conditions.Where(condition => condition.IsActive)
                .All(condition => condition.EvaluateCondition());
        }

        public enum ConditionResult
        {
            NoActiveConditionHolders,
            ConditionsAreMet,
            ConditionsAreNotMet
        }
    }
}