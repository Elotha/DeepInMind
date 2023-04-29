using System;
using System.Collections.Generic;
using EraSoren._Core.Helpers;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public class HopebeamManager : Singleton<HopebeamManager>
    {
        public HopebeamHistory hopebeamHistory;
        public ClickInfoHistory clickInfoHistory;
        public HopebeamTypes hopebeamTypes;
    }
}