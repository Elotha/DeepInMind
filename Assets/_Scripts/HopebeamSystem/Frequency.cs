using System;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    [Serializable]
    public class Frequency
    {
        [SerializeReference] public IProvideFloatField provideFloatField = new ProvideConstantFloat();
        
        public float GetTime()
        {
            return provideFloatField.GetTime();
        }
    }
}