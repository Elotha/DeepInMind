using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EraSoren.HopebeamSystem.SpawnProtocols
{
    public class SpawnRandomlyOutsideOfLevel : HopebeamSpawnProtocol
    {
        [SerializeField] private Vector2 spawnFirstPoint;
        [SerializeField] private Vector2 spawnSecondPoint;

        [Button]
        public override Hopebeam SpawnHopebeam(HopebeamType hopebeamType)
        {
            float rndX, rndY;
            var rnd = Random.Range(0f, 4f);
            switch (rnd)
            {
                case <= 1f:
                    rndX = Random.Range(spawnFirstPoint.x, spawnSecondPoint.x);
                    rndY = spawnFirstPoint.y;
                    break;
                
                case <= 2f:
                    rndX = Random.Range(spawnFirstPoint.x, spawnSecondPoint.x);
                    rndY = spawnSecondPoint.y;
                    break;
                
                case <= 3f:
                    rndX = spawnFirstPoint.x;
                    rndY = Random.Range(spawnFirstPoint.y, spawnSecondPoint.y);
                    break;
                
                default:
                    rndX = spawnSecondPoint.x;
                    rndY = Random.Range(spawnFirstPoint.y, spawnSecondPoint.y);
                    break;
            }
            var spawnPos = new Vector3(rndX, rndY);
            var hopebeamPrefab = hopebeamType.hopebeamPrefab;
            var hopebeam = Instantiate(hopebeamPrefab, spawnPos, quaternion.identity, hopebeamType.transform);
            var hopebeamScript = hopebeam.GetComponent<Hopebeam>();
            hopebeamType.ActivateLifetimeBehaviours(hopebeamScript);
            return hopebeamScript;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            var center = (spawnFirstPoint + spawnSecondPoint) / 2f;
            Gizmos.DrawWireCube(center, spawnSecondPoint - spawnFirstPoint);
        }
    }
}