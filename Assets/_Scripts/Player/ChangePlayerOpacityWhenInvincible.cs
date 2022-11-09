using Sirenix.OdinInspector;
using UnityEngine;

namespace EraSoren.Player
{
    public class ChangePlayerOpacityWhenInvincible : MonoBehaviour
    {
        [MinValue(0f)]
        [MaxValue(1f)]
        [SerializeField] private float alpha;
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        

        public void MakePlayerOpaque()
        {
            ChangePlayerOpacity(1f);
        }

        public void MakePlayerTranslucent()
        {
            ChangePlayerOpacity(alpha);
        }

        private void ChangePlayerOpacity(float newAlpha)
        {
            var color = spriteRenderer.color;
            color.a = newAlpha;
            spriteRenderer.color = color;
        }
        
    }
}