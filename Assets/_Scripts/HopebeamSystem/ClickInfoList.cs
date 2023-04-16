using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public class ClickInfoList : MonoBehaviour
    {
        public List<ClickInfo> clickInfos = new();
        private bool _canCountFrames;
        private int _frameCount;
        
        [Serializable]
        public struct ClickInfo
        {
            public int frameCount;
            public Vector2 clickPositionInWorld;
            public bool isClickMissed;
            public Hopebeam clickedHopebeam;
        }

        [Button]
        public void SetCanCountTime(bool canCount)
        {
            _canCountFrames = canCount;
        }

        private void Update()
        {
            CountFrames();
        }

        private void CountFrames()
        {
            if (_canCountFrames)
            {
                _frameCount++;
            }
        }
    }
}