using UnityEngine;

namespace EraSoren.HopebeamSystem.InteractionBehaviours
{
    public class GiveDamageToHopebeam : HopebeamInteractionBehaviour
    {
        [SerializeField] private int damageNumber = 1;

        public override void Interact(Hopebeam hopebeam, Vector2 catchingPos)
        {
            hopebeam.lives = Mathf.Max(hopebeam.lives - damageNumber, 0);
        }
    }
}