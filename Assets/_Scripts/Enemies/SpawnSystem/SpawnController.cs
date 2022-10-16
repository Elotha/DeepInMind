using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.Enemies.SpawnSystem
{
    public class SpawnController : MonoBehaviour
    {
        public Transform spawnBubblesParent;
        public Transform enemiesParent;
        
        public List<EnemyController> boundedEnemies = new();
        public List<EnemyController> boundedSpawnBubbles = new();
        public bool canSpawn;
        public SpawnBehaviour activeSpawnBehaviour;
        
        #region Events

        public UnityEvent<EnemyController> onEnemySpawn;
        public UnityEvent onLastEnemyDie;
        public UnityEvent onSpawnerHit;
        public UnityEvent<EnemyController> onSpawningEnemySpawnBubble;

        #endregion
        

        private void Start()
        {
            StartSpawning();
        }

        private void StartSpawning()
        {
            if (!canSpawn) return;
            activeSpawnBehaviour.Activate();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Bullet")) 
            {
                onSpawnerHit?.Invoke();
            }
        }

        public void SpawnBubbleSpawned(EnemyController enemyController)
        {
            boundedSpawnBubbles.Add(enemyController);
            enemyController.SpawnController = this;
            onSpawningEnemySpawnBubble?.Invoke(enemyController);
        }

        public void EnemySpawned(EnemyController enemyController)
        {
            boundedEnemies.Add(enemyController);
            enemyController.SpawnController = this;
            onEnemySpawn?.Invoke(enemyController);
        }

        public void BoundedEnemyDie(EnemyController enemyController)
        {
            if (boundedEnemies.Contains(enemyController))
            {
                boundedEnemies.Remove(enemyController);
                if (boundedEnemies.Count == 0)
                {
                    onLastEnemyDie?.Invoke();
                }
            }
            else
            {
                Debug.LogError("The provided enemy controller is not in the list!");
            }
        }
        
        public void BoundedSpawnBubbleDestroyed(EnemyController enemyController)
        {
            if (boundedSpawnBubbles.Contains(enemyController))
            {
                boundedSpawnBubbles.Remove(enemyController);
            }
            else
            {
                Debug.LogError("The provided enemy controller is not in the list!");
            }
        }

        public void KillSpawner()
        {
            Debug.Log("kill spawner");
            Destroy(gameObject);
        }
    }
}