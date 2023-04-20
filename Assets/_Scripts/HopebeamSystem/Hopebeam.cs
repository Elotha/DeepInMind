using System;
using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Hopebeam : MonoBehaviour
    {
        public Rigidbody2D rb;
        [SerializeField] private float gizmoLineMultiplier = 1f;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            var pos = (Vector2)transform.position;
            Gizmos.DrawLine(pos,pos + rb.velocity * gizmoLineMultiplier);
        }
    }
}