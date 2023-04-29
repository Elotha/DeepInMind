using System;
using UnityEngine;

namespace EraSoren.HopebeamSystem.InteractionConditions
{
    [Serializable]
    public class StillHasLives : IInteractionCondition
    {
        [field: SerializeField]
        public bool IsActive { get; set; }
        
        public bool EvaluateCondition(Hopebeam hopebeam, Vector2 catchingPos)
        {
            return hopebeam.lives > 0;
        }
    }
}