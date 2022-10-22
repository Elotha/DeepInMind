using System;
using EraSoren._Core.Helpers;
using UnityEngine;

namespace EraSoren.Player
{
    public class PlayerMovement : Singleton<PlayerMovement>
    {
        public float speedMultiplier = 1f;
        public static bool MovementPermission = true;
        public float playerDirection;
        
        [SerializeField] private LayerMask blocksMask;
        [SerializeField] private float collisionDistance = 0.4f;
        [SerializeField] private float moveSpeed;
        [SerializeField] private bool isRunning;

        private float _verSpeed;
        private float _horSpeed;
        private float _verInput;
        private float _horInput;
        [NonSerialized] public Rigidbody2D Rb;

        protected override void Awake()
        {
            base.Awake();
            Rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            HandleInput();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void HandleInput()
        {
            if (!MovementPermission) return;

            _verInput = Input.GetAxisRaw("Vertical");
            _horInput = Input.GetAxisRaw("Horizontal");

            if (_verInput == 0 && _horInput == 0) return;
            
            var vector = new Vector2(_horInput,_verInput);
            var angle = (Ext.TargetAngle(vector, Vector2.zero) + 360f) % 360f;
            // PlayerController.PlayerAnimations.AdjustDirection(angle);
        }

        private void Move()
        {
            if (!MovementPermission) return;
            
            _horSpeed = CanWalk(new Vector2(_horInput, 0f)) ? _horInput : 0f;
            _verSpeed = CanWalk(new Vector2(0f, _verInput)) ? _verInput : 0f;
                
            var movePos = new Vector2(_horSpeed, _verSpeed).normalized * (moveSpeed * speedMultiplier);

            // Player is going to walk
            if (movePos != Vector2.zero) {
                Rb.MovePosition((Vector2) transform.position + movePos);
                isRunning = true;
            }
        }

        private bool CanWalk(Vector2 dir)
        {
            var hit = Physics2D.Raycast(transform.position, dir, collisionDistance, blocksMask);
            return !hit;
        }

        public static void SetMovementPermission(bool permission)
        {
            MovementPermission = permission;
        }
    }
}