using EraSoren._Core;
using EraSoren._Core.Helpers;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.Player
{
    public class PlayerTakingDamage : Singleton<PlayerTakingDamage>
    {
        #region Events

        public UnityEvent<Vector2> onDamageTaken;

        #endregion
        
        public void TakeDamage(float damageAngle)
        {
            if (!DebugManager.I.invincible)
            {
                PlayerLives.AlterLives(-1);
            }
            
            var knockbackVector = (Vector2)Ext.LengthdirXY(1f, damageAngle * 90);
            onDamageTaken?.Invoke(knockbackVector);
        }
    }
}