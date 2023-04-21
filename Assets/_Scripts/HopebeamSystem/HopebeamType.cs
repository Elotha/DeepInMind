using System;
using System.Collections.Generic;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public class HopebeamType : MonoBehaviour
    {
        public bool isActive = true;
        public string hopebeamTypeID;
        public GameObject hopebeamPrefab;
        public HopebeamSpawnProtocol hopebeamSpawnProtocol;
        public List<HopebeamLifetimeBehaviour> lifetimeBehaviours = new();

        public void SetActivityOfHopebeamType(bool active)
        {
            isActive = active;
        }

        public void ActivateLifetimeBehaviours(Hopebeam hopebeam)
        {
            if (!isActive) return;
            
            foreach (var behaviour in lifetimeBehaviours)
            {
                behaviour.Activate(hopebeam);
            }
        }

        public void SpawnHopebeam()
        {
            if (!isActive) return;
            
            hopebeamSpawnProtocol.SpawnHopebeam(this);
        }

    }
}