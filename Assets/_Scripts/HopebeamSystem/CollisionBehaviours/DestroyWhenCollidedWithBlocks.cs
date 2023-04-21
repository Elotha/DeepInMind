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
            Debug.Log("hopebeam destroyed!");
            Destroy(hopebeam.gameObject);
        }

        public override void ProcessTriggerExit(Hopebeam hopebeam, GameObject collidedObject)
        {
            
        }
    }
}