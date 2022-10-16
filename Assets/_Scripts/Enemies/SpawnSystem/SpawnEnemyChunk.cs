using UnityEngine;

namespace EraSoren.Enemies.SpawnSystem
{
    public class SpawnEnemyChunk : SpawnBehaviour
    {
        [SerializeField] private int enemyAmount;
        [SerializeField] private GameObject enemyObject;
        
        private void Start()
        {
            CreateEnemyChunk();
        }

        private void CreateEnemyChunk()
        {
            for (var i = 0; i < enemyAmount; i++) 
            {
                var randomPos = EnemyManager.I.GetRandomPositionInSpawnArea();
                SpawnEnemy(randomPos);
            }
        }

        private void SpawnEnemy(Vector2 randomPos)
        {
            var enemy = Instantiate(enemyObject, randomPos, Quaternion.identity, spawnController.enemiesParent);
            
            var enemyController = enemy.GetComponent<EnemyController>();
            spawnController.EnemySpawned(enemyController);
        }
    }
}