using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<HopebeamCollisionBehaviour> collisionBehaviours = new();

        private HopebeamManager _hopebeamManager;

        private void Start()
        {
            _hopebeamManager = HopebeamManager.I;
        }

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
            
            var hopebeam = hopebeamSpawnProtocol.SpawnHopebeam(this);
            hopebeam.hopebeamType = this;
            _hopebeamManager.hopebeamHistory.CreateEntry(hopebeam);
        }

        public void ProcessTriggerEnter(Hopebeam hopebeam, GameObject collidedObject)
        {
            foreach (var collisionBehaviour in collisionBehaviours
                         .Where(collisionBehaviour => collisionBehaviour.layerToCollide == 
                                                      LayerMask.GetMask(LayerMask.LayerToName(collidedObject.layer))))
            {
                collisionBehaviour.ProcessTriggerEnter(hopebeam, collidedObject);
            }
        }

        public void ProcessTriggerExit(Hopebeam hopebeam, GameObject collidedObject)
        {
            foreach (var collisionBehaviour in collisionBehaviours.Where(collisionBehaviour => collisionBehaviour.layerToCollide == collidedObject.layer))
            {
                collisionBehaviour.ProcessTriggerExit(hopebeam, collidedObject);
            }
        }

    }
}