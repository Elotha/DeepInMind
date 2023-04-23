using System;
using EraSoren._Core.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.Other
{
    public class TimeManager : Singleton<TimeManager>
    {
        [SerializeField] private bool countLevelTime;
        
        public float levelTime;

        #region Events

        public UnityEvent onStartCountingLevelTime;
        public UnityEvent<bool> onPauseCountingLevelTime;
        public UnityEvent onStopCountingLevelTime;

        #endregion

        [Button]
        public void StartCountingLevelTime()
        {
            levelTime = 0f;
            countLevelTime = true;
            onStartCountingLevelTime?.Invoke();
        }

        [Button]
        public void PauseCountingLevelTime()
        {
            countLevelTime = !countLevelTime;
            onPauseCountingLevelTime?.Invoke(countLevelTime);
        }

        [Button]
        public void StopCountingLevelTime()
        {
            levelTime = 0f;
            countLevelTime = false;
            onStopCountingLevelTime?.Invoke();
        }

        private void Update()
        {
            if (countLevelTime)
            {
                levelTime += Time.deltaTime;
            }
        }
    }
}