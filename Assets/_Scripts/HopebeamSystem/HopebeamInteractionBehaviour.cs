using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public abstract class HopebeamInteractionBehaviour : MonoBehaviour
    {
        public abstract void Interact(Hopebeam hopebeam, Vector2 catchingPos);
    }
}