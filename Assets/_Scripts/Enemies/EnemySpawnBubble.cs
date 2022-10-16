using System.Collections;
using EraSoren._Core.GameplayCore.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.Enemies
{
    [RequireComponent(typeof(EnemyController))]
    public class EnemySpawnBubble : MonoBehaviour
    {
        [SerializeField] private GameObject enemyObject;
        [SerializeField] private float spawnTime;

        private EnemyController _spawnBubbleEnemyController;

        #region Events

        public UnityEvent onEnemySpawnBubbleParried;

        #endregion

        private void Awake()
        {
            _spawnBubbleEnemyController = GetComponent<EnemyController>();
        }

        private void Start()
        {
            StartCoroutine(SpawnEnemy());
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            var parryInterface = GetComponent<IParry>();
            if (parryInterface is { IsParry: true }) 
            {
                onEnemySpawnBubbleParried?.Invoke();
            }
        }

        private IEnumerator SpawnEnemy()
        {
            yield return new WaitForSeconds(spawnTime);
            
            var enemy = Instantiate(enemyObject, transform.position, Quaternion.identity, 
                _spawnBubbleEnemyController.SpawnController.transform);
            
            var newEnemyController = enemy.GetComponent<EnemyController>();
            _spawnBubbleEnemyController.SpawnController.EnemySpawned(newEnemyController);
            _spawnBubbleEnemyController.DestroySpawnBubble();
        }
    }
}