using UnityEngine;

namespace EraSoren.Enemies
{
    public class EnemyDeathParticles : MonoBehaviour
    {
        [SerializeField] private GameObject enemyParticles;

        public void CreateEnemyDeathParticles()
        {
            Instantiate(enemyParticles, transform.position, Quaternion.identity, EnemyManager.I.particlesParent);
        }
    }
}