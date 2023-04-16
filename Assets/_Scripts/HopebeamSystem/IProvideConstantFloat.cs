using System;

namespace EraSoren.HopebeamSystem
{
    [Serializable]
    public class ProvideConstantFloat : IProvideFloatField
    {
        public float timeFrequency;

        public float GetTime()
        {
            return timeFrequency;
        }
    }
}