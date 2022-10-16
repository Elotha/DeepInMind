using UnityEngine;

namespace EraSoren.Hopelight
{
    public class CreateBullets : MonoBehaviour
    {
        [SerializeField] private GameObject bulletObject;
        [SerializeField] private Transform bulletParent;
        [SerializeField] private float bulletSpeed = 15f;
        [SerializeField] private Transform cursorObject;
        
        public void CreateBulletsAtHopelightPosition(Transform hopelight)
        {
            var angle = hopelight.localRotation.eulerAngles.z;
            for (var i = 0; i < 4; i++) {
                var q = Quaternion.Euler(0f, 0f, angle);
                var bullet = Instantiate(bulletObject, hopelight.position, q, bulletParent);
                bullet.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle+90));
                var bulletScript = bullet.GetComponent<BulletObject>();
                angle += 90;
                var angleVector = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle),
                    Mathf.Sin(Mathf.Deg2Rad * angle)).normalized;
                bulletScript.rb.velocity = angleVector * bulletSpeed;
            }
        }
    }
}