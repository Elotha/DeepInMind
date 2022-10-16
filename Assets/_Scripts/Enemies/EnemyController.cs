using System;
using EraSoren.Enemies.SpawnSystem;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        [NonSerialized] public SpawnController SpawnController;
        
        #region Events

        public UnityEvent onEnemyDie;

        #endregion

        public void KillEnemy()
        {
            onEnemyDie?.Invoke();
            SpawnController.BoundedEnemyDie(this);
            Destroy(gameObject);
        }

        public void DestroySpawnBubble()
        {
            SpawnController.BoundedSpawnBubbleDestroyed(this);
            Destroy(gameObject);
        }
    }
}