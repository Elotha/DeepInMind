using System;
using System.Linq;
using EraSoren._Core;
using EraSoren._Core.Helpers;
using EraSoren.Player.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.Player
{
    public class PlayerTakeDamage : Singleton<PlayerTakeDamage>
    {
        private ITakeDamageCondition[] _takeDamageConditions;

        #region Events

        public UnityEvent onDamageTaken;
        public UnityEvent<Vector2> onDamageTakenWithDirection;

        #endregion

        protected override void Awake()
        {
            base.Awake();
            _takeDamageConditions = GetComponents<ITakeDamageCondition>();
        }
        
        public void TakeDamage(float damageAngle)
        {
            var canTakeDamage = _takeDamageConditions.All(x => x.CanTakeDamage());
            if (!canTakeDamage) return;
            
            if (!DebugManager.I.invincible)
            {
                PlayerLives.AlterLives(-1);
            }
            
            onDamageTaken?.Invoke();
            
            var knockbackVector = (Vector2)Ext.LengthdirXY(1f, damageAngle * 90);
            onDamageTakenWithDirection?.Invoke(knockbackVector);
        }
    }
}