using System;
using UnityEngine;

namespace EraSoren.Enemies.SpawnSystem
{
    [RequireComponent(typeof(SpawnController))]
    public class SpawnMaxEnemyLimit : MonoBehaviour, ISpawnCondition
    {
        private SpawnController _spawnController;

        [SerializeField] private int maxEnemyAmount = 20;

        private void Start()
        {
            _spawnController = GetComponent<SpawnController>();
        }

        public bool CanSpawn()
        {
            return _spawnController.boundedEnemies.Count < maxEnemyAmount;
        }
    }
}