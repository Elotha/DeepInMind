using System;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public interface ICondition
    {
        public bool EvaluateCondition();
        // public virtual void ProcessCondition() { }
    }
}