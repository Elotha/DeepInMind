using System;
using System.Linq;
using UnityEngine;

namespace EraSoren.HopebeamSystem.InteractionConditions
{
    [Serializable]
    public class LivesCheck : IInteractionCondition
    {
        [field: SerializeField]
        public bool IsActive { get; set; }

        public int livesCount;
        
        public bool EvaluateCondition(Hopebeam hopebeam, Vector2 catchingPos)
        {
            return hopebeam.lives == livesCount;
        }
    }
}