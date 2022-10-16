using EraSoren._Core;
using UnityEngine;

namespace EraSoren.Enemies.SpawnSystem
{
    public class SpawnerDeath : MonoBehaviour
    {
        [SerializeField] private GameObject bossParticles;

        public void Die()
        {
            // TODO: Use SpawnManager.I.SpawnerDie() instead.
            // TODO: Particle objects should be in SpawnManager.
            Instantiate(bossParticles, transform.position, Quaternion.identity, EnemyManager.I.particlesParent);
            Destroy(gameObject);
            LevelManager.IsLevelEnded = true;
        }
        
    }
}