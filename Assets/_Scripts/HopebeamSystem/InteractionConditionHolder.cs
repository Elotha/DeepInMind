using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    [Serializable]
    public class InteractionConditionHolder
    {
        public bool isActive = true;
        [SerializeReference] public List<IInteractionCondition> conditions = new();

        public static ConditionHolder.ConditionResult EvaluateConditionHolders(List<InteractionConditionHolder> conditionHolders, Hopebeam hopebeam, Vector2 catchingPos)
        {
            if (conditionHolders.All(x => !x.isActive))
            {
                return ConditionHolder.ConditionResult.NoActiveConditionHolders;
            }
            
            var result = conditionHolders.Where(x => x.isActive).Any(y => y.AreConditionsMet(hopebeam, catchingPos));
            return result ? ConditionHolder.ConditionResult.ConditionsAreMet : ConditionHolder.ConditionResult.ConditionsAreNotMet;
        }

        public bool AreConditionsMet(Hopebeam hopebeam, Vector2 catchingPos)
        {
            return conditions.Where(condition => condition.IsActive)
                .All(condition => condition.EvaluateCondition(hopebeam, catchingPos));
        }
    }
}