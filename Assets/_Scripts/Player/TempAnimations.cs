using System;
using UnityEngine;

namespace EraSoren.Player
{
    public class TempAnimations : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField] private KeyAnimationPair[] animations;
        
        [Serializable]
        private struct KeyAnimationPair
        {
            public KeyCode keyCode;
            public string animationName;
        }

        private void Update()
        {
            foreach (var animationPair in animations)
            {
                if (Input.GetKeyDown(animationPair.keyCode))
                {
                    animator.Play(animationPair.animationName);
                }
            }
        }
    }
}