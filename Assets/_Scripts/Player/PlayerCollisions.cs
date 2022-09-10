using EraSoren.Player.Interfaces;
using UnityEngine;

namespace EraSoren.Player
{
    public class PlayerCollisions : MonoBehaviour
    {

        #region Events

        public delegate void PlayerTriggerHandler(Collider other);
        public static event PlayerTriggerHandler OnPlayerTriggerEnter;
        public static event PlayerTriggerHandler OnPlayerTriggerExit;
        
        public delegate void PlayerCollisionHandler(Collision other);
        public static event PlayerCollisionHandler OnPlayerCollisionEnter;
        public static event PlayerCollisionHandler OnPlayerCollisionExit;

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITriggeredByPlayer interactable))
            {
                interactable.Interact();
            }
            
            OnPlayerTriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnPlayerTriggerExit?.Invoke(other);
        }

        private void OnCollisionEnter(Collision other)
        {
            OnPlayerCollisionEnter?.Invoke(other);
        }
        
        private void OnCollisionExit(Collision other)
        {
            OnPlayerCollisionExit?.Invoke(other);

        }
    }
}