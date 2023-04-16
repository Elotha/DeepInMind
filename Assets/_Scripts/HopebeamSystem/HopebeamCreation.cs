using System;

namespace EraSoren.HopebeamSystem
{
    [Serializable]
    public class HopebeamCreation
    {
        public HopebeamType hopebeamType;
        public float delayTime;
        public NotValidException notValidException;
    }
}