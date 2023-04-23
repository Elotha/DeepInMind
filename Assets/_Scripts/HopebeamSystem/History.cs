using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.HopebeamSystem
{
    public class History<T> : MonoBehaviour where T : class
    {
        public List<T> history;
        
        #region Events

        public UnityEvent onClearHistory;

        #endregion

        [Button]
        public virtual void ClearHistory()
        {
            onClearHistory?.Invoke();
            history.Clear();
        }
    }
}