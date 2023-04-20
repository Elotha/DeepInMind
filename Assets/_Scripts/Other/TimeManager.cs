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
        }

        [Button]
        public void StopCountingLevelTime()
        {
            levelTime = 0f;
            countLevelTime = false;
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