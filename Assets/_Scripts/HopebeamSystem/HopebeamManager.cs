using System;
using System.Collections.Generic;
using EraSoren._Core.Helpers;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public class HopebeamManager : Singleton<HopebeamManager>
    {
        public List<HopebeamTypeHolder> hopebeamTypes = new();
        public ClickInfoList clickInfoList;

        [Serializable]
        public struct HopebeamTypeHolder
        {
            public HopebeamType hopebeamType;
            public bool solo;
            public bool mute;
        }

    }
}