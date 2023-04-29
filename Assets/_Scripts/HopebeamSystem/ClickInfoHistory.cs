using System;
using EraSoren._InputSystem;
using EraSoren.Other;

namespace EraSoren.HopebeamSystem
{
    public class ClickInfoHistory : History<ClickInfo>
    {
        public bool canSaveInfo;
        public int successfulInteractionCount;
        public int missedInteractionCount;

        private TimeManager _timeManager;

        private void Start()
        {
            _timeManager = TimeManager.I;
        }

        private void Update()
        {
            if (!canSaveInfo) return;
            ProcessInput();
        }

        private void ProcessInput()
        {
            if (!InputManager.GetKeyDown(InputButton.Catch)) return;
            
            var catchingInfo = CatchingManager.I.TryToCatch();
            history.Add(new ClickInfo(_timeManager.frameCount, catchingInfo));
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