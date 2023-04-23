using System;
using EraSoren.HopebeamSystem.TimeSetters;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    [Serializable]
    public class HopebeamCreation
    {
        public string hopebeamTypeID;
        [SerializeReference] public IProvideTime delayTime = new ProvideConstantTime();
        public NotValidException notValidException;
    }
}