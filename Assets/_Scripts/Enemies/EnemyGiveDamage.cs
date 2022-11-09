using System.Collections;
using EraSoren.Player;
using UnityEngine;

namespace EraSoren.Enemies
{
    public class EnemyGiveDamage : MonoBehaviour
    {
        private EnemyState _enemyState;
        private EnemyMovement _enemyMovement;
        private bool _damageIsGiven;
        private int _direction;

        [SerializeField] private float damageCooldownTime = 1f;
        

        private void Start()
        {
            _enemyState = GetComponentInParent<EnemyState>();
            _enemyMovement = GetComponentInParent<EnemyMovement>();
            _enemyMovement.OnNewDestinationSet += MovementDirection;
        }

        private void OnDestroy()
        {
            _enemyMovement.OnNewDestinationSet -= MovementDirection;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_enemyState.enemyState == EnemyStates.Attacking)
            {
                if (!_damageIsGiven)
                {
                    PlayerTakeDamage.I.TakeDamage(Mathf.FloorToInt(_direction / 90f));
                    _damageIsGiven = true;
                    StartCoroutine(DamageCooldown());
                }
            }
        }

        private IEnumerator DamageCooldown()
        {
            yield return new WaitForSeconds(damageCooldownTime);
            _damageIsGiven = false;
        }

        private void MovementDirection(int angle)
        {
            _direction = angle;
        }
        
    }
}