using System.Collections;
using UnityEngine;

namespace EraSoren.Enemies
{
    public class ParticlesObject : MonoBehaviour
    {
        [SerializeField] private float lifeTime;
        
        private void Start()
        {
            StartCoroutine(DestroyParticles());
        }

        private IEnumerator DestroyParticles()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }
    }
}