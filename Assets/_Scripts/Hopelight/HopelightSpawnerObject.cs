using UnityEngine;

namespace EraSoren.Hopelight
{
    public class HopelightSpawnerObject : MonoBehaviour
    {
        public bool isActive = true;
        public bool isInCooldown;
        public float cooldownTime;

        private float _cooldown;
    }
}