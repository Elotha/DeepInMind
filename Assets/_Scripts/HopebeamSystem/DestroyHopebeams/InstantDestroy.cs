using UnityEngine;

namespace EraSoren.HopebeamSystem.DestroyHopebeams
{
    public class InstantDestroy : MonoBehaviour, IDestroyHopebeams
    {
        public void StartDestroySequence(Hopebeam hopebeam)
        {
            Debug.Log("hopebeam destroyed!");
            
            // TODO: Back to the pool
            Destroy(hopebeam.gameObject);
            // gameObject.SetActive(false);
            // hopebeam.hopebeamState = HopebeamState.Poolable;
        }
    }
}