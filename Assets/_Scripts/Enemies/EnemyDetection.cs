using EraSoren.Enemies.Interfaces;
using UnityEngine;

namespace EraSoren.Enemies
{
    public class EnemyDetection : MonoBehaviour, IDetectPlayer
    {
        private bool _isPlayerInRegion;
        
        [SerializeField] private LayerMask detectionLayer;
        
        public bool IsPlayerInRegion()
        {
            return _isPlayerInRegion;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _isPlayerInRegion = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _isPlayerInRegion = false;
        }
    }
}