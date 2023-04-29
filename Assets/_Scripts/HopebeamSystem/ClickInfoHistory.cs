using System;
using System.Linq;
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
            CatchingManager.I.onPrimaryInputCatch += CreateEntry;
        }
        private void CreateEntry(CatchingManager.CatchingInfo catchingInfo)
        {
            if (!canSaveInfo) return;
            history.Add(new ClickInfo(_timeManager.frameCount, _timeManager.levelTime, catchingInfo));
            if (catchingInfo.hopebeams.Count > 0)
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
        public float timeStamp;
        public CatchingManager.CatchingInfo catchingInfo;

        public ClickInfo(int frameCount, float timeStamp, CatchingManager.CatchingInfo catchingInfo)
        {
            this.frameCount = frameCount;
            this.timeStamp = timeStamp;
            this.catchingInfo = catchingInfo;
        }
    }
}