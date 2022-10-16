using System.Collections;
using EraSoren._Core.Helpers;
using EraSoren.Enemies.Interfaces;
using UnityEngine;

namespace EraSoren.Enemies
{
    public class EnemyStandardAttack : MonoBehaviour, IEnemyAttack
    {
        private EnemyState _enemyState;
        private EnemyMovement _enemyMovement;
        
        [SerializeField] private float spriteChangeTime;
        [SerializeField] private float attackTime;
        [SerializeField] private float afterAttackTime;
        [SerializeField] private GameObject attackSystemObject;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float projectileOffset;

        public event IEnemyAttack.AttackHandler OnAttackEnd;

        private void Awake()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
            _enemyState = GetComponent<EnemyState>();
            _enemyMovement.OnNewDestinationSet += SetAttackObjectRotation;
        }

        private void OnDestroy()
        {
            _enemyMovement.OnNewDestinationSet -= SetAttackObjectRotation;
        }
        public void Attack()
        {
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            StartCasting();

            yield return new WaitForSeconds(attackTime);
            if (_enemyState.enemyState != EnemyStates.Casting)  { yield break; }
            StartAttacking();

            yield return new WaitForSeconds(afterAttackTime);
            if (_enemyState.enemyState != EnemyStates.Attacking) { yield break; }
            FinishAttacking();
        }
        
        private void StartCasting()
        {
            _enemyState.enemyState = EnemyStates.Casting;
        }

        private void StartAttacking()
        {
            _enemyState.enemyState = EnemyStates.Attacking;
            var transform1 = transform;
            var rot = attackSystemObject.transform.localRotation;
            var offset = Ext.LengthdirXY(projectileOffset, rot.eulerAngles.z);
            var projectile = Instantiate(projectilePrefab, transform1.position + offset, rot);
        }

        private void FinishAttacking()
        {
            OnAttackEnd?.Invoke();
        }

        private void SetAttackObjectRotation(int angle)
        {
            attackSystemObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
    }
}