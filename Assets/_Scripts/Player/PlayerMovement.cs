using System;
using EraSoren._Core.GameplayCore.Interfaces;
using EraSoren._Core.Helpers;
using EraSoren._InputSystem;
using UnityEngine;

namespace EraSoren.Player
{
    public class PlayerMovement : Singleton<PlayerMovement>
    {
        public float speedMultiplier = 1f;
        public float playerDirection;
        
        [SerializeField] private LayerMask blocksMask;
        [SerializeField] private float moveSpeed;
        // [SerializeField] private bool isRunning;

        private float _verSpeed;
        private float _horSpeed;
        private float _verInput;
        private float _horInput;
        [NonSerialized] public Rigidbody2D Rb;

        private IWalkable _walkable;

        protected override void Awake()
        {
            base.Awake();
            Rb = GetComponent<Rigidbody2D>();
            _walkable = GetComponent<IWalkable>();
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
            if (!_walkable.MovementPermission) return;

            var movementInput = DefaultInput.MovementInput;
            _horInput = movementInput.x;
            _verInput = movementInput.y;

            if (_horInput == 0 && _verInput == 0) return;
            
            var vector = new Vector2(_horInput,_verInput);
            var angle = (Ext.TargetAngle(vector, Vector2.zero) + 360f) % 360f;
            // PlayerController.PlayerAnimations.AdjustDirection(angle);
        }

        private void Move()
        {
            if (!_walkable.MovementPermission) return;
            
            _horSpeed = _walkable.CanWalkOntoPosition(new Vector2(_horInput, 0f)) ? _horInput : 0f;
            _verSpeed = _walkable.CanWalkOntoPosition(new Vector2(0f, _verInput)) ? _verInput : 0f;
                
            var movePos = new Vector2(_horSpeed, _verSpeed).normalized * (moveSpeed * speedMultiplier);
            
            if (movePos == Vector2.zero) return;
            
            Rb.MovePosition((Vector2) transform.position + movePos);
            // isRunning = true;
        }

        public void SetMovementPermission(bool permission)
        {
            _walkable.MovementPermission = permission;
        }
    }
}