using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public abstract class HopebeamSpawnProtocol : MonoBehaviour
    {
        public abstract Hopebeam SpawnHopebeam(HopebeamType hopebeamType);
    }
}