using System;
using RegularDuck._Core;
using RegularDuck._Core.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RegularDuck._Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")] [SerializeField] private bool gainSpeedOnlyWhileRunning;

        [SerializeField] private float accelerationSpeed = 10;
        public float runSpeed = 7;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private bool isRunning = true;

        [NonSerialized] public BoxCollider BoxCollider;
        [NonSerialized] public Rigidbody Rb;

        #region Grounding Fields

        [NonSerialized] public bool IsGrounded;
        [NonSerialized] public bool IsMovementOverriden;

        private const float _skinWidth = 0.015f;
        private const float _rayGap = .2f;

        private int _verticalRayCount;
        private float _verticalRaySpacing;
        private Vector3 _raycastOrigin;

        private bool _wasGrounded;

        #endregion

        #region Events

        public delegate void GroundHandler();

        public event GroundHandler OnTouchGround;
        public event GroundHandler OnLeaveGround;

        public delegate void RunningHandler();

        public event RunningHandler OnStopRunning;
        public event RunningHandler OnContinueRunning;

        #endregion

        protected void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            BoxCollider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            Rb.drag = PhysicsManager.I.linearDrag;

            CalculateRaySpacing();

            Rb.velocity = new Vector3(0f, 0f, runSpeed);
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            // Don't move until start input is given
            // if (!GameManager.I.isStartInputPressed) return;

            // Let another movement system handle movement
            if (IsMovementOverriden) return;

            // Check for ground and call corresponding functions
            GroundCheck();
            if (!_wasGrounded && IsGrounded)
            {
                TouchGround();
            }
            else if (_wasGrounded && !IsGrounded)
            {
                LeaveGround();
            }

            RunnerMovement();

            FallCheck();
        }

        private void FallCheck()
        {
            if (!IsGrounded && Ext.Format(Rb.velocity.y, 3) < 0f)
            {
                PlayerController.PlayerStates.SetCurrentState(PlayerStates.States.FlyToFall);
                LeaveGround();
            }
        }

        private void RunnerMovement()
        {
            if (!gainSpeedOnlyWhileRunning || (gainSpeedOnlyWhileRunning && IsGrounded))
            {
                if (Rb.velocity.z < runSpeed &&
                    isRunning)
                {
                    Rb.AddForce(Vector3.forward * accelerationSpeed);
                }
                else if (Rb.velocity.z > runSpeed)
                {
                    // var velocity = rb.velocity;
                    // velocity = new Vector3(runSpeed, velocity.y, velocity.z);
                    // rb.velocity = velocity;
                    // rb.AddForce(Vector3.back * accelerationSpeed);
                }
            }
        }

        private void TouchGround()
        {
            // PlayerController.PlayerStates.SetCurrentState(PlayerStates.States.Run);
            OnTouchGround?.Invoke();
        }

        public void LeaveGround()
        {
            IsGrounded = false;
            OnLeaveGround?.Invoke();
        }


        private void GroundCheck()
        {
            // Don't need to check for ground when movement is handled externally
            if (IsMovementOverriden) return;

            // Cache last grounding state for later use
            _wasGrounded = IsGrounded;

            IsGrounded = false;

            // Don't need to check when going up
            if (Rb.velocity.y * Time.fixedDeltaTime > 0.1f) return;

            UpdateRaycastOrigin();

            const float rayLength = 0.1f + _skinWidth;

            for (var i = 0; i < _verticalRayCount; i++)
            {
                Vector3 rayOrigin = _raycastOrigin;
                rayOrigin += Vector3.forward * (_verticalRaySpacing * i);

                var ray = new Ray(rayOrigin, Vector3.down);
                var hit = Physics.Raycast(ray, out RaycastHit hitInfo, rayLength, groundLayerMask);

                Debug.DrawRay(rayOrigin, Vector3.down * rayLength, Color.red);

                if (hit)
                {
                    if (hitInfo.collider.isTrigger) continue;

                    IsGrounded = true;
                    return;
                }
            }
        }

        private void UpdateRaycastOrigin()
        {
            // Shift bounds in by skin width
            Bounds bounds = BoxCollider.bounds;
            bounds.Expand(_skinWidth * -2);

            // Start raycasts from bottom left of collider
            _raycastOrigin = bounds.min;
        }

        private void CalculateRaySpacing()
        {
            // Shift bounds in by skin width
            Bounds bounds = BoxCollider.bounds;
            bounds.Expand(_skinWidth * -2);

            // Get width
            var boundsWidth = bounds.size.x;

            // Calculate ray count, make sure ray count is greater than or equal to 2
            _verticalRayCount = Mathf.Max(Mathf.RoundToInt(boundsWidth / _rayGap), 2);

            // Calculate spacing so rays are evenly distributed
            _verticalRaySpacing = bounds.size.x / (_verticalRayCount - 1);
        }

        public void OverrideMovement(bool state)
        {
            IsMovementOverriden = state;
        }

        [Button]
        public void StopRunning()
        {
            isRunning = false;
            Rb.velocity = Vector3.zero;

            if (!IsMovementOverriden)
                OnStopRunning?.Invoke();
        }

        [Button]
        public void ContinueRunning()
        {
            isRunning = true;

            if (!IsMovementOverriden)
                OnContinueRunning?.Invoke();
        }
    }
}