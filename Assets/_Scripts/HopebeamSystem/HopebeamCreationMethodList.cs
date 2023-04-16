using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace EraSoren.HopebeamSystem
{
    public class HopebeamCreationMethodList : MonoBehaviour
    {
        public List<HopebeamCreationMethod> hopebeamCreationMethods = new();
        public bool isActive;
        
        public void SetCreationActivity(bool active)
        {
            isActive = active;
        }
    }
}