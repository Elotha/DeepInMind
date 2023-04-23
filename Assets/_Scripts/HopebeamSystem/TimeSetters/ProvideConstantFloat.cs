using System;

namespace EraSoren.HopebeamSystem.TimeSetters
{
    [Serializable]
    public class ProvideConstantTime : IProvideTime
    {
        public float time;

        public float GetTime()
        {
            return time;
        }
    }
}