using System;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.Enemies.SpawnSystem
{
    [RequireComponent(typeof(SpawnController))]
    public abstract class SpawnBehaviour : MonoBehaviour
    {
        [SerializeField] protected SpawnController spawnController;

        public bool isActive;

        protected virtual void Awake()
        {
            if (spawnController == null)
                spawnController = GetComponent<SpawnController>();
        }

        public virtual void Activate()
        {
            isActive = true;
        }

        public virtual void Deactivate()
        {

            isActive = false;
        }
    }
}