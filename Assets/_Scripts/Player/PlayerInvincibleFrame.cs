using System;
using System.Collections;
using EraSoren.Player.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.Player
{
    public class PlayerInvincibleFrame : MonoBehaviour, ITakeDamageCondition
    {
        [SerializeField] private float invincibilityTime;

        private bool _canTakedamage = true;
        private Coroutine _invincibilityCooldownCoroutine;

        #region Events

        public UnityEvent onInvincibilityStart;
        public UnityEvent onInvincibilityEnd;

        #endregion

        public bool CanTakeDamage()
        {
            return _canTakedamage;
        }

        public void BeInvincibleForAWhile()
        {
            _canTakedamage = false;
            
            if (_invincibilityCooldownCoroutine != null)
                StopCoroutine(_invincibilityCooldownCoroutine);

            _invincibilityCooldownCoroutine = StartCoroutine(InvincibilityCooldown());
            
            onInvincibilityStart?.Invoke();
        }

        private IEnumerator InvincibilityCooldown()
        {
            yield return new WaitForSeconds(invincibilityTime);
            _canTakedamage = true;
            
            onInvincibilityEnd?.Invoke();
        }
    }
}