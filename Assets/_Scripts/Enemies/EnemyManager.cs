using EraSoren._Core.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EraSoren.Enemies
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        [HideInInspector] public float[] limitX = new float[2];
        [HideInInspector] public float[] limitY = new float[2];
        public Transform enemyParent;
        public Transform particlesParent;

        protected override void Awake()
        {
            base.Awake();

            SetLimits();
        }

        private void SetLimits()
        {
            var localScale = transform.localScale;
            limitX[0] = -localScale.x / 2;
            limitX[1] = localScale.x / 2;
            limitY[0] = -localScale.y / 2;
            limitY[1] = localScale.y / 2;
        }

        public Vector2 GetRandomPositionInSpawnArea()
        {
            var rndX = Random.Range(limitX[0],limitX[1]);
            var rndY = Random.Range(limitY[0],limitY[1]);
            var randomPos = new Vector2(rndX,rndY);
            return randomPos;
        }
    }
}