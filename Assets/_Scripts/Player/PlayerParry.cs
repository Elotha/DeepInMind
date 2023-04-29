using System.Collections;
using EraSoren._Core.Audio;
using EraSoren._Core.GameplayCore.Interfaces;
using EraSoren._InputSystem;
using EraSoren.Player.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace EraSoren.Player
{
    public class PlayerParry : MonoBehaviour, IParry
    {
        public float parryLockTime;
        public float parryTime;
        public float cooldownTime;

        public bool IsParry { get; set; }

        private bool _canParry = true;
        private IParryFeedback _parryFeedback;

        #region Events
        public delegate void ParryHandler();
        public event ParryHandler OnParryStart;
        public event ParryHandler OnParryEnd;
        
        public UnityEvent onParried;

        #endregion

        private void Start()
        {
            _parryFeedback = GetComponent<IParryFeedback>();
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (!_canParry) return;
            if (InputManager.GetKeyDown(InputButton.Secondary))
            {
                StartCoroutine(Parry());
                
            }
        }

        private IEnumerator Parry()
        {
            _canParry = false;
            IsParry = true;
            PlayerMovement.I.SetMovementPermission(false);
            _parryFeedback.ParryStartFeedback();
            OnParryStart?.Invoke();
            
            yield return new WaitForSecondsRealtime(parryLockTime);
            _parryFeedback.ParryLockEndedFeedback();
            PlayerMovement.I.SetMovementPermission(true);
            
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
            onParried?.Invoke();
        }
    }
}