using System;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Hopebeam : MonoBehaviour
    {
        [HideInInspector] public HopebeamType hopebeamType;
        [HideInInspector] public Rigidbody2D rb;
        public HopebeamState hopebeamState = HopebeamState.BeingCreated;
        public int lives = 1;
        public bool interactable = true;
        public int catchPriority = 1;
        
        [Header("Debug")]
        [SerializeField] private float gizmoLineMultiplier = 1f;
        
        private IDestroyHopebeams _destroyHopebeam;
        
        private void Awake()
        {
            // TODO: No get component
            _destroyHopebeam = GetComponent<IDestroyHopebeams>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            hopebeamType.ProcessTriggerEnter(this, col.gameObject);
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            hopebeamType.ProcessTriggerExit(this, col.gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            var pos = (Vector2)transform.position;
            Gizmos.DrawLine(pos,pos + rb.velocity * gizmoLineMultiplier);
        }

        public void StartDestroySequence()
        {
            hopebeamState = HopebeamState.BeingDestroyed;
            _destroyHopebeam.StartDestroySequence(this);
        }
    }
}