using EraSoren.Enemies;
using UnityEngine;

namespace EraSoren.Hopelight
{
    public class BulletObject : MonoBehaviour
    {
        [HideInInspector] public Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
            {
                var angle = -Vector2.SignedAngle(rb.velocity, Vector2.right);
                var enemyScript = other.transform.GetComponent<EnemyController>();
                
                if (enemyScript != null) 
                    enemyScript.KillEnemy();
                
                Destroy(gameObject);
            }
            
        }
    }
}