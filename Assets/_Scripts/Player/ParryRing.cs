using System.Collections;
using EraSoren.Player.Interfaces;
using UnityEngine;

namespace EraSoren.Player
{
    public class ParryRing : MonoBehaviour, IParryFeedback
    {
        [SerializeField] private SpriteRenderer parryRingRenderer;

        private PlayerParry _playerParry;

        private void Start()
        {
            _playerParry = GetComponent<PlayerParry>();
        }

        public void ParryStartFeedback()
        {
            StartCoroutine(ParryRingCoroutine());
        }

        public void ParryLockEndedFeedback()
        {
            
        }

        public void ParryEndFeedback()
        {
            
        }

        public void ParryCooldownFeedback()
        {
            
        }
        

        private IEnumerator ParryRingCoroutine()
        {
            var color = parryRingRenderer.color;
            color.a = 0.5f;
            parryRingRenderer.color = color;
            var time = 0f;
            var cooldownTime = _playerParry.cooldownTime;
            while (time < cooldownTime) {
                time = Mathf.Min(time + Time.unscaledDeltaTime, cooldownTime);
                parryRingRenderer.sharedMaterial.SetFloat("_Arc1",360f - (time / cooldownTime) * 360f);
                yield return null;
            }
            parryRingRenderer.sharedMaterial.SetFloat("_Arc1",0f);
            color.a = 1f;
            parryRingRenderer.color = color;
        }
    }
}