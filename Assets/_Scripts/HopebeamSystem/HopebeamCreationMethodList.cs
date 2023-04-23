using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace EraSoren.HopebeamSystem
{
    public class HopebeamCreationMethodList : MonoBehaviour
    {
        public List<HopebeamCreationMethod> hopebeamCreationMethods = new();
        private bool _isActivatedByCreator;
        
        public void SetCreationActivity(bool active)
        {
            _isActivatedByCreator = active;
            foreach (var method in hopebeamCreationMethods)
            {
                method.SetCreationActivity(active);
            }
        }

        public void RestartAllNextCreationTimes()
        {
            foreach (var method in hopebeamCreationMethods)
            {
                method.RestartNextCreationTime();
            }
        }
    }
}