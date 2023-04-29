using System;
using EraSoren.Other;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public class HopebeamHistory : History<HopebeamHistoryEntry>
    {
        private TimeManager _timeManager;

        private void Start()
        {
            _timeManager = TimeManager.I;
        }

        public void CreateEntry(Hopebeam hopebeam)
        {
            var time = _timeManager.levelTime;
            var entry = new HopebeamHistoryEntry(hopebeam, time);
            history.Add(entry);
        }
    }
        
    [Serializable]
    public class HopebeamHistoryEntry
    {
        public Hopebeam hopebeam;
        public float timeStamp;

        public HopebeamHistoryEntry(Hopebeam hopebeam, float timeStamp)
        {
            this.hopebeam = hopebeam;
            this.timeStamp = timeStamp;
        }
    }
}