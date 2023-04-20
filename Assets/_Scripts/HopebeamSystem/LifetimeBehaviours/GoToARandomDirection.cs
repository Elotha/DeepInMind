using EraSoren._Core.Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EraSoren.HopebeamSystem.LifetimeBehaviours
{
    public class GoToARandomDirection : HopebeamLifetimeBehaviour
    {
        [SerializeField] private Vector2 gameFieldFirstPoint;
        [SerializeField] private Vector2 gameFieldSecondPoint;
        [SerializeField] private float velocityMultiplier;

        public override void Activate(Hopebeam hopebeam)
        {
            var hopebeamPos = hopebeam.transform.position;
            var rndX = Random.Range(gameFieldFirstPoint.x, gameFieldSecondPoint.x);
            var rndY = Random.Range(gameFieldFirstPoint.y, gameFieldSecondPoint.y);
            var rndPos = new Vector3(rndX, rndY);
            var dir = (rndPos - hopebeamPos).normalized;
            hopebeam.rb.velocity = dir * velocityMultiplier;
            var angle = -Vector2.SignedAngle(dir, Vector2.right);
            hopebeam.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            var center = (gameFieldSecondPoint + gameFieldFirstPoint) / 2f;
            Gizmos.DrawWireCube(center, gameFieldSecondPoint - gameFieldFirstPoint);
        }
    }
}