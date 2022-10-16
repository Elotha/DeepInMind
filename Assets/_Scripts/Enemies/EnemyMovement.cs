using EraSoren._Core.GameplayCore.Interfaces;
using EraSoren._Core.Helpers;
using EraSoren.Enemies.Interfaces;
using UnityEngine;

namespace EraSoren.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Vector2 _destination;
        private EnemyState _enemyState;
        public IWalkable Walkable;
        private IChoosePath _choosePath;

        [SerializeField] private float moveSpeed = 0.4f;

        #region Events

        public delegate void DestinationHandler(int angle);
        public event DestinationHandler OnNewDestinationSet;
        
        #endregion

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            _enemyState = GetComponent<EnemyState>();
            Walkable = GetComponent<IWalkable>();
            _choosePath = GetComponent<IChoosePath>();
        }

        private void Start()
        {
            _destination = ChooseNewDestination();
        }

        private void FixedUpdate()
        {
            if (CanMove())
                Move();
        }

        private Vector2 ChooseNewDestination()
        {
            var destination = _choosePath.ChooseNewDestination();
            var direction = destination.normalized;
            var index = Mathf.FloorToInt(Ext.TargetAngle(direction, Vector2.zero));
            index = (index + 360) % 360;
            OnNewDestinationSet?.Invoke(index);
            return (Vector2)transform.position + destination;
        }

        private bool CanMove()
        {
            return _enemyState.enemyState is EnemyStates.Searching or EnemyStates.Cooldown 
                   && (Walkable.MovementPermission);
        }

        private void Move()
        {
            var isStopped = true;
            var pos = (Vector2)transform.position;
            if (pos != _destination)
            {
                var targetPos = Vector2.MoveTowards(pos, _destination, 
                    moveSpeed * Time.fixedDeltaTime * 50f);

                var canWalk = Walkable.CanWalkOntoPosition(targetPos - pos);
                if (canWalk) 
                {
                    _rb.MovePosition(targetPos);
                    isStopped = false;
                }
            }

            if (isStopped)
            {
                _destination = ChooseNewDestination();
            }
        }
    }
}