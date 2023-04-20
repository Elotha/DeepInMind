using System;
using System.Collections.Generic;
using EraSoren._InputSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public class ClickInfoList : MonoBehaviour
    {
        [SerializeField] private bool canCountFrames;
        public List<ClickInfo> clickInfos = new();
        private int _frameCount;
        
        [Serializable]
        public struct ClickInfo
        {
            public int frameCount;
            public CatchingManager.CatchingInfo catchingInfo;

            public ClickInfo(int frameCount, CatchingManager.CatchingInfo catchingInfo)
            {
                this.frameCount = frameCount;
                this.catchingInfo = catchingInfo;
            }
        }

        // [Button]
        public void SetCanCountTime(bool canCount)
        {
            canCountFrames = canCount;
        }

        private void Update()
        {
            CountFrames();
            ProcessInput();
        }

        private void CountFrames()
        {
            if (canCountFrames)
            {
                _frameCount++;
            }
        }

        private void ProcessInput()
        {
            if (InputManager.GetKeyDown(InputButton.Catch))
            {
                var catchingInfo = CatchingManager.I.TryToCatch();
                clickInfos.Add(new ClickInfo(_frameCount, catchingInfo));
            }
        }
    }
}