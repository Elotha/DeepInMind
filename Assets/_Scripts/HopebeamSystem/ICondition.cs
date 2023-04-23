using System;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public interface ICondition
    {
        public bool IsActive { get; set; }
        public bool EvaluateCondition();
    }
}