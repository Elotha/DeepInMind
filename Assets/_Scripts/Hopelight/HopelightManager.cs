using System.Collections;
using EraSoren._Core;
using EraSoren._Core.Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EraSoren.Hopelight
{
    public class HopelightManager : Singleton<HopelightManager>
    {
        public HopelightSpawner currentSpawner;
        
        [SerializeField] private float creationOffsetX;
        [SerializeField] private float creationOffsetY;
        [SerializeField] private float minRandomTime;
        [SerializeField] private float maxRandomTime;
        [SerializeField] private GameObject hopelightObject;

        public float lifeTime;
        public float moveSpeed;
        
        private void Start()
        {
            StartCoroutine(SpawnHopelight());
        }

        private IEnumerator SpawnHopelight()
        {
            while (!LevelManager.IsLevelEnded) {
                var time = Random.Range(minRandomTime, maxRandomTime);
                yield return new WaitForSeconds(time);
                var pos = RandomSpawnPosition();
                Instantiate(hopelightObject, pos, Quaternion.identity, transform);
            }
        }

        private Vector2 RandomSpawnPosition()
        {
            var rnd = Random.Range(0, 25);
            Vector2 pos;
            if (rnd < 16) {
                var rnd2 = Random.Range(0, 2);
                pos.x = Random.Range(-creationOffsetX, creationOffsetX);
                pos.y = (rnd2 == 0) ? creationOffsetY : -creationOffsetY;
            }
            else {
                var rnd3 = Random.Range(0, 2);
                pos.x = (rnd3 == 0) ? creationOffsetX : -creationOffsetX;
                pos.y = Random.Range(-creationOffsetY, creationOffsetY);
            }

            return pos;
        }
    }
}