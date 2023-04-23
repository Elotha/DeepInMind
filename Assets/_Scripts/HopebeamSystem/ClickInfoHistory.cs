using System;
using System.Collections.Generic;
using EraSoren._InputSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public class ClickInfoHistory : History<ClickInfo>
    {
        [SerializeField] private bool canCountFrames;
        public int successfulInteractionCount;
        public int missedInteractionCount;
        private int _frameCount;
        public void SetCanCountTime(bool canCount)
        {
            canCountFrames = canCount;
        }

        private void Update()
        {
            if (!canCountFrames) return;
            
            CountFrames();
            ProcessInput();
        }

        private void CountFrames()
        {
            _frameCount++;
        }

        private void ProcessInput()
        {
            if (!InputManager.GetKeyDown(InputButton.Catch)) return;
            
            var catchingInfo = CatchingManager.I.TryToCatch();
            history.Add(new ClickInfo(_frameCount, catchingInfo));
            if (catchingInfo.didPlayerCatchBeam)
            {
                successfulInteractionCount++;
            }
            else
            {
                missedInteractionCount++;
            }
        }
    }
        
    [Serializable]
    public class ClickInfo
    {
        public int frameCount;
        public CatchingManager.CatchingInfo catchingInfo;

        public ClickInfo(int frameCount, CatchingManager.CatchingInfo catchingInfo)
        {
            this.frameCount = frameCount;
            this.catchingInfo = catchingInfo;
        }
    }
}