using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public abstract class HopebeamSpawnProtocol : MonoBehaviour
    {
        public abstract Hopebeam SpawnHopebeam(HopebeamType hopebeamType);
        protected static void SetHopebeamStateToActive(Hopebeam hopebeamScript)
        {
            hopebeamScript.hopebeamState = HopebeamState.Active;
        }
    }
}