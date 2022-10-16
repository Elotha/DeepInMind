using System;
using UnityEngine;

namespace EraSoren.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] private float tolerance = 0.4f;
        
        [NonSerialized] public SpriteRenderer SpriteRenderer;

        private Animator _animator;

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            if (_animator.runtimeAnimatorController != null)
            {
                RunTriggers();
            }
        }

        private void RunTriggers()
        {
            var playerVelocity = PlayerController.PlayerMovement.Rb.velocity;
            _animator.SetBool(PlayerAnimationHashes.VerSpeed, Mathf.Abs((float)playerVelocity.y) > tolerance);
            _animator.SetBool(PlayerAnimationHashes.HorSpeed, Mathf.Abs((float)playerVelocity.x) > tolerance);
        }

        public void PlayAnimation(PlayerStates.States state)
        {
            if (_animator.runtimeAnimatorController == null) return;
            
            switch (state)
            {
                default:
                    _animator.Play("Character" + state);
                    break;
            }
        }

        public void PlayAnimation(int animationHash, float speed = 1f)
        {
            _animator.SetFloat(PlayerAnimationHashes.AnimSpeed, speed);
            _animator.Play(animationHash);
        }
    }
}