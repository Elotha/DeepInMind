using EraSoren.Enemies.Interfaces;
using UnityEngine;

namespace EraSoren.Enemies
{
    public class EnemyAttackCooldown : MonoBehaviour, ICanDetectPlayer
    {
        private EnemyState _enemyState;
        private EnemyAttack _enemyAttack;
        private float _cooldownCountdown;

        [SerializeField] private float maxCooldownTime;
        
        private void Start()
        {
            _enemyState = GetComponent<EnemyState>();
            _enemyAttack = GetComponent<EnemyAttack>();
            _enemyAttack.attack.OnAttackEnd += EndCooldown;
            EndCooldown();
        }

        private void Update()
        {
            Cooldown();
        }

        private void OnDestroy()
        {
            _enemyAttack.attack.OnAttackEnd -= EndCooldown;
        }

        private void Cooldown()
        {
            if (_cooldownCountdown > 0f)
            {
                if (_enemyState.enemyState == EnemyStates.Cooldown)
                {
                    _cooldownCountdown = Mathf.Max(_cooldownCountdown - Time.deltaTime, 0f);
                    if (_cooldownCountdown == 0f)
                    {
                        _enemyState.enemyState = EnemyStates.Searching;
                    }
                }
                else
                {
                    _cooldownCountdown = 0f;
                }
            }
        }

        public bool CanDetectPlayer()
        {
            return _cooldownCountdown == 0f;
        }

        private void EndCooldown()
        {
            _cooldownCountdown = maxCooldownTime;
            _enemyState.enemyState = EnemyStates.Cooldown;
        }
    }
}