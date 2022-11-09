using System;
using EraSoren.Enemies.SpawnSystem;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        [NonSerialized] public SpawnController SpawnController;

        private bool _isEnemyDead;
        
        #region Events

        public UnityEvent onEnemyDie;

        #endregion

        public void KillEnemy()
        {
            if (_isEnemyDead)
                return;

            _isEnemyDead = true;
            
            onEnemyDie?.Invoke();
            SpawnController.BoundedEnemyDie(this);
            Destroy(gameObject);
        }

        public void DestroySpawnBubble()
        {
            if (_isEnemyDead)
                return;

            _isEnemyDead = true;
            
            SpawnController.BoundedSpawnBubbleDestroyed(this);
            Destroy(gameObject);
        }
    }
}