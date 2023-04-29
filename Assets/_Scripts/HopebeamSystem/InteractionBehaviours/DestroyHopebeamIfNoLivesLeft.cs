using UnityEngine;

namespace EraSoren.HopebeamSystem.InteractionBehaviours
{
    public class DestroyHopebeamIfNoLivesLeft : HopebeamInteractionBehaviour
    {
        public override void Interact(Hopebeam hopebeam, Vector2 catchingPos)
        {
            hopebeam.StartDestroySequence();
        }
    }
}