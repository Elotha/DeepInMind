using System;
using EraSoren.HopebeamSystem.TimeSetters;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    [Serializable]
    public class TimeSetter
    {
        [SerializeReference] public IProvideTime provideTime = new ProvideConstantTime();
        
        public float GetTime()
        {
            return provideTime.GetTime();
        }
    }
}