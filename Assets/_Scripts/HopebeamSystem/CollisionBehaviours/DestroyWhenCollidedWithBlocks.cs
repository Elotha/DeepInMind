using UnityEngine;

namespace EraSoren.HopebeamSystem.CollisionBehaviours
{
    public class DestroyWhenCollidedWithBlocks : HopebeamCollisionBehaviour
    {
        public override void Activate(Hopebeam hopebeam)
        {
            
        }

        public override void ProcessTriggerEnter(Hopebeam hopebeam, GameObject collidedObject)
        {
            hopebeam.StartDestroySequence();
        }

        public override void ProcessTriggerExit(Hopebeam hopebeam, GameObject collidedObject)
        {
            
        }
    }
}