using System.Collections;
using EraSoren._Core;
using EraSoren._Core.Audio;
using EraSoren._Core.GameplayCore.Interfaces;
using EraSoren._InputSystem;
using EraSoren.Player.Interfaces;
using UnityEngine;

namespace EraSoren.Player
{
    public class PlayerParry : MonoBehaviour, IParry
    {
        public float parryLockTime;
        public float parryTime;
        public float cooldownTime;

        private bool _canParry = true;
        private IParryFeedback _parryFeedback;

        public bool IsParry { get; private set; }

        #region Events
        
        public event IParry.ParryHandler OnParryStart;
        public event IParry.ParryHandler OnParryEnd;
        public event IParry.ParryHandler OnParried;

        #endregion

        private void Start()
        {
            _parryFeedback = GetComponent<IParryFeedback>();
            OnParried += PlayParrySound;
        }

        private void OnDestroy()
        {
            OnParried -= PlayParrySound;
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (!_canParry) return;
            if (InputManager.GetKeyDown(InputButton.Parry))
            {
                Debug.Log("parry button down");
                StartCoroutine(Parry());
            }
        }

        private IEnumerator Parry()
        {
            _canParry = false;
            IsParry = true;
            PlayerMovement.SetMovementPermission(false);
            _parryFeedback.ParryStartFeedback();
            OnParryStart?.Invoke();
            
            yield return new WaitForSecondsRealtime(parryLockTime);
            _parryFeedback.ParryLockEndedFeedback();
            PlayerMovement.SetMovementPermission(true);
            
            yield return new WaitForSecondsRealtime(parryTime - parryLockTime);
            _parryFeedback.ParryEndFeedback();
            IsParry = false;
            OnParryEnd?.Invoke();
            
            yield return new WaitForSeconds(cooldownTime - parryTime);
            _canParry = true;
            _parryFeedback.ParryCooldownFeedback();
        }

        private static void PlayParrySound()
        {
            SoundManager.PlaySound(SoundEffects.Parry, SoundManager.I.defaultSource);
        }
        public void ApplyParry()
        {
            OnParried?.Invoke();
        }

        public void TakeDamage(float damageAngle)
        {
            PlayerTakingDamage.I.TakeDamage(damageAngle);
        }
    }
}