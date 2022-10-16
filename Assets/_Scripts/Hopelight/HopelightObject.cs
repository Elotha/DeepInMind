using System.Collections;
using EraSoren._Core.Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EraSoren.Hopelight
{
    public class HopelightObject : MonoBehaviour
    {
        [SerializeField] private float directionOffsetX;
        [SerializeField] private float directionOffsetY;

        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            var dir = GetRandomPosition();
            var angle = -Vector2.SignedAngle(dir - (Vector2)transform.position, Vector2.right);
            _rb.velocity = Ext.LengthdirXY(HopelightManager.I.moveSpeed,angle);
            transform.localRotation = Quaternion.Euler(new Vector3(0f,0f,angle));

            StartCoroutine(Destroy());
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(HopelightManager.I.lifeTime);
            Destroy(gameObject);
        }

        private Vector2 GetRandomPosition()
        {
            var rndX = Random.Range(-directionOffsetX,directionOffsetX);
            var rndY = Random.Range(-directionOffsetY,directionOffsetY);
            var randomPos = new Vector2(rndX,rndY);
            return randomPos;
        }
    }
}