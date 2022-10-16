using UnityEngine;

namespace EraSoren.Hopelight
{
    public class DestroyHopelightObject : MonoBehaviour
    {
        public void DestroyHopelightObjectOnTeleport(Transform hit)
        {
            Destroy(hit.gameObject);
        }
    }
}