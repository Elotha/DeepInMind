using System;
using RegularDuck._Core.Helpers;
using UnityEngine;

namespace RegularDuck._Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerStates : MonoBehaviour
    {
        [NonSerialized] public bool IsMovableState = true;
        [NonSerialized] public bool IsVulnerable = true;

        private PlayerAnimations _playerAnimations;

        // Enums
        public enum States
        {
            Idle,
            Run,
            Jump,
            Fly,
            FlyToFall,
            Fall,
            Death
        }

        public States CurrentState
        {
            get => _currentState;
            private set
            {
                if (_currentState == value) return;
                _currentState = value;
                _playerAnimations.PlayAnimation(_currentState);
            }
        }
        
        private States _currentState;
    
        protected void Awake()
        {
            _playerAnimations = GetComponent<PlayerAnimations>();
        }

        public void SetCurrentState(States state)
        {
            CurrentState = state;
        }
    }
}