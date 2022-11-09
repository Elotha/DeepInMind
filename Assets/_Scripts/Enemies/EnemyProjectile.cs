using System.Collections;
using EraSoren._Core.GameplayCore.Interfaces;
using EraSoren._Core.Helpers;
using EraSoren.Player;
using EraSoren.Player.Interfaces;
using UnityEngine;

namespace EraSoren.Enemies
{
    public class EnemyProjectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float lifeTimeWithoutParry;
        [SerializeField] private float lifeTimeAfterParry;

        private Rigidbody2D _rb;
        private Coroutine _destroyCoroutine;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.velocity = Ext.LengthdirXY(speed, transform.rotation.eulerAngles.z);
            _destroyCoroutine = StartCoroutine(WithoutParryLife());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var parryInterface = other.GetComponent<IParry>();
            if (parryInterface != null)
            {
                if (parryInterface.IsParry)
                {
                    parryInterface.ApplyParry();
                    ApplyParry();
                }
                else
                {
                    GiveDamage();
                }
            }
        }

        private void GiveDamage()
        {
            PlayerTakeDamage.I.TakeDamage(transform.rotation.z);
            Destroy(gameObject);
        }

        private void ApplyParry()
        {
            var angle = transform.localRotation.eulerAngles.z;
            transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 180f));
            var velocity = _rb.velocity;
            _rb.velocity = new Vector2(-velocity.x, -velocity.y) * 2f;
            StartCoroutine(AfterParry());
            if (_destroyCoroutine != null)
            {
                StopCoroutine(_destroyCoroutine);
            }
        }

        private IEnumerator AfterParry()
        {
            yield return new WaitForSeconds(lifeTimeAfterParry);
            Destroy(gameObject);
        }

        private IEnumerator WithoutParryLife()
        {
            yield return new WaitForSeconds(lifeTimeWithoutParry);
            Destroy(gameObject);
        }
    }
}