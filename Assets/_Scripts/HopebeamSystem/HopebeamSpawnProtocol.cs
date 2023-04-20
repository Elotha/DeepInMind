using UnityEngine;

namespace EraSoren.HopebeamSystem
{
    public abstract class HopebeamSpawnProtocol : MonoBehaviour
    {
        public abstract void SpawnHopebeam(HopebeamType hopebeamType);
    }
}