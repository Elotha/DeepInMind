using System;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public interface IInteractionCondition
    {
        public bool IsActive { get; set; }
        public bool EvaluateCondition(Hopebeam hopebeam, Vector2 catchingPos);
    }
}