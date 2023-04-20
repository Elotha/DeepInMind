using System;
using System.Collections.Generic;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public class HopebeamType : MonoBehaviour
    {
        public GameObject hopebeamPrefab;
        public HopebeamSpawnProtocol hopebeamSpawnProtocol;
        public List<HopebeamLifetimeBehaviour> lifetimeBehaviours = new();

        public void ActivateLifetimeBehaviours(Hopebeam hopebeam)
        {
            foreach (var behaviour in lifetimeBehaviours)
            {
                behaviour.Activate(hopebeam);
            }
        }

        public void SpawnHopebeam()
        {
            hopebeamSpawnProtocol.SpawnHopebeam(this);
        }

    }
}