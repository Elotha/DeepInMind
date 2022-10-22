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
        
        [SerializeField] private float attackTime;
        [SerializeField] private float afterAttackTime;
        [SerializeField] private GameObject attackSystemObject;
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float spriteChangeTime;
        [SerializeField] private Sprite[] sprites;

        #region Events
        public event IEnemyAttack.AttackHandler OnCastingStart;
        public event IEnemyAttack.AttackHandler OnAttack;
        public event IEnemyAttack.AttackHandler OnAttackEnd;

        #endregion

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
            OnCastingStart?.Invoke();

            yield return new WaitForSeconds(spriteChangeTime);
            SetSpriteToAttack();

            yield return new WaitForSeconds(attackTime - spriteChangeTime);
            if (_enemyState.enemyState != EnemyStates.Casting)  { yield break; }
            StartAttacking();
            OnAttack?.Invoke();

            yield return new WaitForSeconds(afterAttackTime);
            if (_enemyState.enemyState != EnemyStates.Attacking) { yield break; }
            FinishAttacking();
            OnAttackEnd?.Invoke();
        }
        
        private void StartCasting()
        {
            _enemyState.enemyState = EnemyStates.Casting;
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = sprites[0];
        }

        private void SetSpriteToAttack()
        {
            spriteRenderer.sprite = sprites[1];
        }

        private void StartAttacking()
        {
            _enemyState.enemyState = EnemyStates.Attacking;
        }

        private void FinishAttacking()
        {
            spriteRenderer.enabled = false;
        }

        private void SetAttackObjectRotation(int angle)
        {
            attackSystemObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
    }
}