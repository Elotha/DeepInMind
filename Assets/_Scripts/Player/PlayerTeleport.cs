using System.Collections;
using EraSoren.Enemies;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.Player
{
    public class PlayerTeleport : MonoBehaviour
    {
        [SerializeField] private float lockTime;

        #region Events

        // public UnityEvent onTeleport;

        #endregion

        private IEnumerator TeleportCoroutine(Vector3 position)
        {
            PlayerMovement.SetMovementPermission(false);
            position.x = Mathf.Clamp(position.x, EnemyManager.I.limitX[0], EnemyManager.I.limitX[1]);
            position.y = Mathf.Clamp(position.y, EnemyManager.I.limitY[0], EnemyManager.I.limitY[1]);
            PlayerController.PlayerMovement.Rb.MovePosition(position);
            
            // onTeleport?.Invoke();
            
            yield return new WaitForSeconds(lockTime);
            
            PlayerMovement.SetMovementPermission(true);
        }

        public void StartTeleporting(Transform hit)
        {
            StartCoroutine(TeleportCoroutine(hit.position));
        }
    }
}