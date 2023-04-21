using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public abstract class HopebeamCollisionBehaviour : MonoBehaviour
    {
        public abstract void Activate(Hopebeam hopebeam);
        public LayerMask layerToCollide;
        public abstract void ProcessTriggerEnter(Hopebeam hopebeam, GameObject collidedObject);
        public abstract void ProcessTriggerExit(Hopebeam hopebeam, GameObject collidedObject);
    }
}