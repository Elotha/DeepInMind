using UnityEngine;

namespace EraSoren.HopebeamSystem.InteractionBehaviours
{
    public class TeleportCharacter : HopebeamInteractionBehaviour
    {
        [SerializeField] private Transform character;
        
        public override void Interact(Hopebeam hopebeam, Vector2 catchingPos)
        {
            character.transform.position = catchingPos;
        }
    }
}