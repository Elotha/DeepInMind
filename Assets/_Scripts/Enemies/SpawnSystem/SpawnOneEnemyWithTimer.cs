using System;
using System.Linq;
using EraSoren._Core.Helpers;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace EraSoren.Enemies.SpawnSystem
{
    public class SpawnOneEnemyWithTimer : SpawnBehaviour
    {
        public GameObject spawnBubbleObject;
        [SerializeField] private float spawnRadius;
        [SerializeField] private float spawnTime;
        [SerializeField] private float oneTimeDelay;
        
        [SerializeField] private SpriteRenderer sprRenderer;

        private float _spawnCountdown;
        private ISpawnCondition[] _spawnConditions;

        private void Start()
        {
            _spawnConditions = GetComponents<ISpawnCondition>();
        }

        private void Update()
        {
            if (!isActive) return;
            SpawnCooldown();
        }

        private void SpawnCooldown()
        {
            // TODO: I will use an interface to give feedback to the player.
            
            // spawnCountdown / spawnTime should be obtainable.
            sprRenderer.sharedMaterial.SetFloat("_Arc1",
                360f - (_spawnCountdown / spawnTime) * 360f);

            if (_spawnCountdown < spawnTime)
                _spawnCountdown = Mathf.Min(_spawnCountdown + Time.deltaTime, spawnTime);
                
            if (_spawnCountdown >= spawnTime)
            {
                var canSpawn = _spawnConditions.All(x => x.CanSpawn());
                if (canSpawn)
                {
                    SpawnEnemySpawnBubble();
                    _spawnCountdown = 0;
                }
            }
        }

        private void SpawnEnemySpawnBubble()
        {
            var angle = Random.Range(0f, 360f);
            var randomPos = Ext.LengthdirXY(spawnRadius, angle);
            var spawnBubble = Instantiate(spawnBubbleObject, transform.position + randomPos, 
                Quaternion.identity, spawnController.spawnBubblesParent);
            
            var enemyController = spawnBubble.GetComponent<EnemyController>();
            spawnController.SpawnBubbleSpawned(enemyController);
        }

        public void AddDelay()
        {
            _spawnCountdown = Mathf.Max(_spawnCountdown - oneTimeDelay, 0f);
        }
    }
}