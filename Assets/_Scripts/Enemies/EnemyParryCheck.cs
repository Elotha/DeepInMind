using System.Collections;
using EraSoren._Core.GameplayCore.Interfaces;
using EraSoren.Player;
using EraSoren.Player.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.Enemies
{
    public class EnemyParryCheck : MonoBehaviour
    {
        private EnemyState _enemyState;

        #region Events
        
        public UnityEvent onParried;

        #endregion

        private void Start()
        {
            _enemyState = GetComponentInParent<EnemyState>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            CheckParry(other);
        }

        private void CheckParry(Collider2D other)
        {
            if (_enemyState.enemyState != EnemyStates.Casting &&
                _enemyState.enemyState != EnemyStates.Attacking) return;
            
            var parryInterface = other.GetComponent<IParry>();
            if (parryInterface is not { IsParry: true }) return;

            parryInterface.ApplyParry();
            onParried?.Invoke();
        }
    }
}