using UnityEngine;

namespace EraSoren.Enemies
{
    public class EnemyAnimations : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;

        [HideInInspector] public SpriteRenderer sprRenderer;

        private EnemyMovement _enemyMovement;

        private void Awake()
        {
            sprRenderer = GetComponent<SpriteRenderer>();
            _enemyMovement = GetComponent<EnemyMovement>();
            _enemyMovement.OnNewDestinationSet += ChangeDirection;
        }

        private void OnDestroy()
        {
            _enemyMovement.OnNewDestinationSet -= ChangeDirection;
        }

        private void ChangeDirection(int angle)
        {
            var index = Mathf.FloorToInt(angle / 90f);
            sprRenderer.sprite = sprites[index];
        }
    }
}