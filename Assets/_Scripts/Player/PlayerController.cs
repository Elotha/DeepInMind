using UnityEngine;

namespace RegularDuck._Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerAnimations PlayerAnimations;
        [SerializeField] private PlayerAnimations playerAnimations;
        
        public static PlayerCollisions PlayerCollisions;
        [SerializeField] private PlayerCollisions playerCollisions;
        
        public static PlayerMovement PlayerMovement;
        [SerializeField] private PlayerMovement playerMovement;
        
        public static PlayerStates PlayerStates;
        [SerializeField] private PlayerStates playerStates;

        private void Awake()
        {
            PlayerAnimations = playerAnimations;
            PlayerCollisions = playerCollisions;
            PlayerMovement = playerMovement;
            PlayerStates = playerStates;
        }
    }
}